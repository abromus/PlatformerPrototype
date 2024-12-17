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

        private readonly WeaponInfo[] _weaponInfos;

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

            InitShootingDelay(shootingMode);

            index = _currentIndex;

            return true;
        }

        public void AddAmmo(int count)
        {
            UnityEngine.Debug.Log($"Add {count} ammo");
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void InitRandomWeapon()
        {
            _currentIndex = UnityEngine.Random.Range(0, _weaponInfos.Length);
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
