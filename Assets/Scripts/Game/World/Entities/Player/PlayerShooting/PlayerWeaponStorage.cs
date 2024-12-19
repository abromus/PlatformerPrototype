using PlatformerPrototype.Game.Configs;

namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerWeaponStorage : IPlayerWeaponStorage
    {
        private int _currentIndex;
        private bool _canShooting;
        private bool _isPaused;
        private float _shootingDelay;
        private float _maxShootingDelay;
        private int _currentAmmo;

        private readonly WeaponInfo[] _weaponInfos;

        public int CurrentAmmo => _currentAmmo;

        public event System.Action AmmoChanged;

        internal PlayerWeaponStorage(in WeaponInfo[] weaponInfos)
        {
            _weaponInfos = weaponInfos;

            _canShooting = true;

            InitRandomWeapon();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick(float deltaTime)
        {
            CheckShootingDelay(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Restart()
        {
            InitRandomWeapon();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool TryShoot(ShootingMode shootingMode, out int index)
        {
            index = -1;

            if (_isPaused || _canShooting == false)
                return false;

            _canShooting = false;

            --_currentAmmo;

            InitShootingDelay(shootingMode);

            index = _currentIndex;

            AmmoChanged?.Invoke();

            return true;
        }

        public void AddAmmo(int count)
        {
            _currentAmmo += count;

            AmmoChanged?.Invoke();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void InitRandomWeapon()
        {
            _currentIndex = UnityEngine.Random.Range(0, _weaponInfos.Length);
            _currentAmmo = _weaponInfos[_currentIndex].Capacity;

            AmmoChanged?.Invoke();
        }

        private void CheckShootingDelay(float deltaTime)
        {
            if (_canShooting)
                return;

            _shootingDelay += deltaTime;

            if (_maxShootingDelay < _shootingDelay)
                _canShooting = true;
        }

        private void InitShootingDelay(ShootingMode shootingMode)
        {
            _shootingDelay = 0f;
            _maxShootingDelay = shootingMode == ShootingMode.Single
                ? _weaponInfos[_currentIndex].SingleShootingDelay
                : _weaponInfos[_currentIndex].ContinuousShootingDelay;
        }
    }
}
