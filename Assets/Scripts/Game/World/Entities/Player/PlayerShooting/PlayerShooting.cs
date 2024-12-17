namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerShooting : IPlayerShooting
    {
        private bool _isShooting;
        private int _currentIndex;

        private readonly IPlayerInput _playerInput;
        private readonly UnityEngine.Transform _transform;
        private readonly Factories.IProjectileFactory _factory;
        private readonly UnityEngine.Transform _projectileContainer;
        private readonly Configs.WeaponInfo[] _weaponInfos;

        private readonly IPlayerWeaponStorage _weaponStorage;
        private readonly Core.IObjectPool<Projectiles.IProjectile> _pool;
        private readonly System.Collections.Generic.List<Projectiles.IProjectile> _projectiles = new(64);

        internal PlayerShooting(in PlayerShootingArgs args)
        {
            _playerInput = args.PlayerInput;
            _transform = args.Transform;
            _factory = args.ProjectileFactory;
            _projectileContainer = args.ProjectileContainer;
            _weaponInfos = args.PlayerConfig.WeaponConfig.WeaponInfos;

            _weaponStorage = new PlayerWeaponStorage(in _weaponInfos);

            _pool = new Core.ObjectPool<Projectiles.IProjectile>(CreateProjectile);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick(float deltaTime)
        {
            CheckInput();

            _weaponStorage.Tick(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void FixedTick(float deltaTime)
        {
            if (_isShooting == false)
                return;

            if (_weaponStorage.TryShoot(_playerInput.ShootingMode, out var index))
                InitProjectile(index);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            _weaponStorage.SetPause(isPaused);
        }

        public void Restart()
        {
            for (int i = 0; i < _projectiles.Count; i++)
                _pool.Release(_projectiles[i]);

            _projectiles.Clear();

            _weaponStorage.Restart();
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
            var projectilePrefab = _weaponInfos[_currentIndex].ProjectilePrefab;
            var projectile = _factory.Create(projectilePrefab, _projectileContainer);
            projectile.Destroyed += OnProjectileDestroyed;

            _projectiles.Add(projectile);

            return projectile;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void CheckInput()
        {
            _isShooting = _playerInput.IsShooting;
        }

        private void InitProjectile(int index)
        {
            _currentIndex = index;

            var projectileOffset = _weaponInfos[_currentIndex].ProjectileOffset;
            var projectile = _pool.Get();
            var direction = _transform.localScale.x == Constants.Left ? Constants.Left : Constants.Right;
            var position = _transform.position + direction * projectileOffset;
            projectile.InitPosition(position, direction);
            projectile.Destroyed += OnProjectileDestroyed;
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
