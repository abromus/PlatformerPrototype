namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerShooting : IPlayerShooting
    {
        private bool _isShooting;
        private ShootingMode _shootingMode;
        private int _currentIndex;

        private readonly IPlayerInput _playerInput;
        private readonly UnityEngine.Transform _transform;
        private readonly UnityEngine.Transform _weaponTransform;
        private readonly Factories.IProjectileFactory _factory;
        private readonly UnityEngine.Transform _projectileContainer;
        private readonly Configs.WeaponInfo[] _weaponInfos;

        private readonly IPlayerWeaponStorage _weaponStorage;
        private readonly Animators.IPlayerShootingAnimator _animator;
        private readonly UnityEngine.Animator _animatorView;
        private readonly Core.IObjectPool<Projectiles.IProjectile> _pool;
        private readonly System.Collections.Generic.List<Projectiles.IProjectile> _projectiles = new(64);

        public int CurrentAmmo => _weaponStorage.CurrentAmmo;

        public event System.Action AmmoChanged;

        public event System.Action AmmoOut;

        internal PlayerShooting(in PlayerShootingArgs args)
        {
            _playerInput = args.PlayerInput;
            _transform = args.Transform;
            _weaponTransform = args.WeaponTransform;
            _factory = args.ProjectileFactory;
            _projectileContainer = args.ProjectileContainer;
            _weaponInfos = args.PlayerConfig.WeaponConfig.WeaponInfos;
            _animatorView = args.AnimatorView;

            _weaponStorage = new PlayerWeaponStorage(in _weaponInfos);
            _animator = new Animators.PlayerShootingAnimator(_animatorView);

            _pool = new Core.ObjectPool<Projectiles.IProjectile>(CreateProjectile);

            Subscribe();
        }

        public void Tick(float deltaTime)
        {
            CheckInput();

            _weaponStorage.Tick(deltaTime);
        }

        public void FixedTick(float deltaTime)
        {
            if (_isShooting == false)
                return;

            if (_weaponStorage.CurrentAmmo == 0)
            {
                AmmoOut?.Invoke();
            }
            else if (_weaponStorage.TryShoot(_shootingMode, out var index))
            {
                InitProjectile(index);

                StartAnimateShoot();
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void LateTick(float deltaTime)
        {
            _animator.LateTick(deltaTime);
        }

        public void SetPause(bool isPaused)
        {
            _weaponStorage.SetPause(isPaused);
            _animator.SetPause(isPaused);
        }

        public void Restart()
        {
            for (int i = 0; i < _projectiles.Count; i++)
            {
                var projectile = _projectiles[i];
                projectile.Deactivate();
                projectile.Clear();

                _pool.Release(projectile);
            }

            _projectiles.Clear();

            _weaponStorage.Restart();
        }

        public void AddAmmo(int count)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(0 < count);
#endif

            _weaponStorage.AddAmmo(count);
        }

        public void StopAnimation()
        {
            _animator.Stop();
        }

        public void Destroy()
        {
            Unsubscribe();

            for (int i = 0; i < _projectiles.Count; i++)
            {
                var projectile = _projectiles[i];
                projectile.Deactivate();
                projectile.Clear();

                _pool.Release(projectile);
            }

            _projectiles.Clear();
            _pool.Destroy();
        }

        private Projectiles.IProjectile CreateProjectile()
        {
            var projectilePrefab = _weaponInfos[_currentIndex].ProjectilePrefab;
            var projectile = _factory.Create(projectilePrefab, _projectileContainer);

            _projectiles.Add(projectile);

            return projectile;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void CheckInput()
        {
            _isShooting = _playerInput.IsShooting;
            _shootingMode = _playerInput.ShootingMode;
        }

        private void InitProjectile(int index)
        {
            _currentIndex = index;

            var projectileOffset = _weaponInfos[_currentIndex].ProjectileOffset;
            var projectile = _pool.Get();
            var direction = _transform.localScale.x == Constants.Left ? Constants.Left : Constants.Right;
            var position = _weaponTransform.position + direction * projectileOffset;
            projectile.InitPosition(position, direction);
            projectile.Activate();
            projectile.Destroyed += OnProjectileDestroyed;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void StartAnimateShoot()
        {
            _animator.Animate();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _weaponStorage.AmmoChanged += OnAmmoChanged;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Unsubscribe()
        {
            if (_weaponStorage != null)
                _weaponStorage.AmmoChanged -= OnAmmoChanged;
        }

        private void OnAmmoChanged()
        {
            AmmoChanged?.Invoke();
        }

        private void OnProjectileDestroyed(Projectiles.IProjectile projectile)
        {
            projectile.Destroyed -= OnProjectileDestroyed;
            projectile.Deactivate();
            projectile.Clear();

            _projectiles.Remove(projectile);
            _pool.Release(projectile);
        }
    }
}
