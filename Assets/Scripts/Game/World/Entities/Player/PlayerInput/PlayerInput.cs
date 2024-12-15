namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerInput : IPlayerInput
    {
        private UnityEngine.Vector2 _moveDirection;
        private bool _isShooting;
        private bool _isPaused;

        private readonly Core.Services.IInputService _inputService;
        private readonly UnityEngine.Vector2 _zeroPosition;

        public UnityEngine.Vector2 MoveDirection => _moveDirection;

        public bool IsShooting => _isShooting;

        internal PlayerInput(Core.Services.IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Tick(float deltaTime)
        {
            CheckInput();
        }

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

            _isShooting = _inputService.GetKey(UnityEngine.KeyCode.Space);
        }
    }
}
