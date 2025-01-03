﻿namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerInput : IPlayerInput
    {
        private float _moveXDirection;
        private bool _isShooting;
        private ShootingMode _shootingMode;
        private bool _isPaused;

        private readonly Services.IEventSystemService _eventSystemService;
        private readonly Core.Services.IInputService _inputService;

        public float MoveXDirection => _moveXDirection;

        public bool IsShooting => _isShooting;

        public ShootingMode ShootingMode => _shootingMode;

        internal PlayerInput(Services.IEventSystemService eventSystemService, Core.Services.IInputService inputService)
        {
            _eventSystemService = eventSystemService;
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
            _moveXDirection = _isPaused ? Constants.Zero : _inputService.GetHorizontalAxisRaw();

            _shootingMode = _inputService.GetLeftMouseButtonDown()
                ? ShootingMode.Single
                : _inputService.GetLeftMouseButton()
                    ? ShootingMode.Continuous
                    : ShootingMode.None;

            _isShooting = _shootingMode != ShootingMode.None && _eventSystemService.IsPointerOverGameObject() == false;
        }
    }
}
