namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerInput : IPlayerInput
    {
        private UnityEngine.Vector2 _moveDirection;
        private bool _isShooting;
        private ShootingMode _shootingMode;
        private bool _isPaused;

        private readonly Core.Services.IInputService _inputService;
        private readonly UnityEngine.Vector2 _zeroPosition;

        public UnityEngine.Vector2 MoveDirection => _moveDirection;

        public bool IsShooting => _isShooting;

        public ShootingMode ShootingMode => _shootingMode;

        internal PlayerInput(Core.Services.IInputService inputService)
        {
            _inputService = inputService;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick(float deltaTime)
        {
            CheckInput();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        private void CheckInput()
        {
            if (_isPaused == false)
            {
                _moveDirection.x = _inputService.GetHorizontalAxisRaw();
                _moveDirection.y = _inputService.GetVerticalAxisRaw();
            }
            else
            {
                _moveDirection = _zeroPosition;
            }

            _shootingMode = _inputService.GetLeftMouseButtonDown()
                ? ShootingMode.Single
                : _inputService.GetLeftMouseButton()
                    ? ShootingMode.Continuous
                    : ShootingMode.None;

            _isShooting = _shootingMode != ShootingMode.None;
        }
    }

    internal enum ShootingMode
    {
        None = 0,
        Single = 1,
        Continuous = 2,
    }
}
