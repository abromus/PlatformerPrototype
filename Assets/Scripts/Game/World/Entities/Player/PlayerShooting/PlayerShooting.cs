namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerShooting : IPlayerShooting
    {
        private bool _isShooting;
        private bool _canShooting;
        private bool _isPaused;
        private float _shootingDelay;
        private float _maxShootingDelay;

        private readonly IPlayerInput _playerInput;
        private readonly UnityEngine.Transform _transform;
        private readonly Factories.IProjectileFactory _factory;
        private readonly Projectiles.BaseProjectile _projectilePrefab;
        private readonly UnityEngine.Transform _projectileContainer;
        private readonly float _singleShootingDelay;
        private readonly float _continuousShootingDelay;
        private readonly UnityEngine.Vector3 _projectileOffset;
        private readonly Core.IObjectPool<Projectiles.IProjectile> _pool;
        private readonly System.Collections.Generic.List<Projectiles.IProjectile> _projectiles = new(64);

        internal PlayerShooting(in PlayerShootingArgs args)
        {
            _playerInput = args.PlayerInput;
            _transform = args.Transform;
            _factory = args.ProjectileFactory;
            _projectilePrefab = args.ProjectilePrefab;
            _projectileContainer = args.ProjectileContainer;

            var playerConfig = args.PlayerConfig;
            _singleShootingDelay = playerConfig.SingleShootingDelay;
            _continuousShootingDelay = playerConfig.ContinuousShootingDelay;
            _projectileOffset = playerConfig.ProjectileOffset;
            _pool = new Core.ObjectPool<Projectiles.IProjectile>(CreateProjectile);

            _canShooting = true;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick(float deltaTime)
        {
            CheckInput();
            CheckShootingDelay(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void FixedTick(float deltaTime)
        {
            TryShoot();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        public void Destroy()
        {
            for (int i = 0; i < _projectiles.Count; i++)
                _pool.Release(_projectiles[i]);

            _projectiles.Clear();

            _pool.Destroy();
        }

        private Projectiles.IProjectile CreateProjectile()
        {
            var projectile = _factory.Create(_projectilePrefab, _projectileContainer);
            projectile.Destroyed += OnProjectileDestroyed;

            _projectiles.Add(projectile);

            return projectile;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void CheckInput()
        {
            _isShooting = _playerInput.IsShooting;
        }

        private bool TryShoot()
        {
            if (_isPaused || _canShooting == false)
                return false;

            if (_isShooting)
            {
                _canShooting = false;

                InitProjectile();
                InitShootingDelay();

                return true;
            }

            return false;
        }

        private void InitProjectile()
        {
            var projectile = _pool.Get();
            var position = _transform.position + _projectileOffset;
            var direction = _transform.localScale.x == Constants.Left ? Constants.Left : Constants.Right;
            projectile.InitPosition(position, direction);
        }

        private void InitShootingDelay()
        {
            var shootingMode = _playerInput.ShootingMode;
            _shootingDelay = 0f;
            _maxShootingDelay = shootingMode == ShootingMode.Single ? _singleShootingDelay : _continuousShootingDelay;
        }

        private void CheckShootingDelay(float deltaTime)
        {
            if (_canShooting)
                return;

            _shootingDelay += deltaTime;

            if (_maxShootingDelay < _shootingDelay)
                _canShooting = true;
        }

        private void OnProjectileDestroyed(Projectiles.IProjectile projectile)
        {
            projectile.Destroyed -= OnProjectileDestroyed;
            projectile.Clear();

            _projectiles.Remove(projectile);
            _pool.Release(projectile);
        }
    }
}
