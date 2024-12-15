namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerMovement : IPlayerMovement
    {
        private UnityEngine.Vector2 _moveDirection;
        private bool _isPaused;

        private readonly IPlayerInput _playerInput;
        private readonly UnityEngine.Transform _transform;
        private readonly UnityEngine.Vector2 _movementSensitivity;

        internal PlayerMovement(IPlayerInput playerInput, UnityEngine.Transform transform, Configs.IPlayerConfig playerConfig)
        {
            _playerInput = playerInput;
            _transform = transform;
            _movementSensitivity = playerConfig.MovementSensitivity;
        }

        public void Tick(float deltaTime)
        {
            CheckInput();
        }

        public void FixedTick(float deltaTime)
        {
            Move(deltaTime);
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        private void CheckInput()
        {
            _moveDirection = _playerInput.MoveDirection;
        }

        private void Move(float deltaTime)
        {
            if (_isPaused)
                return;

            var position = _transform.position;
            position.x += _moveDirection.x * _movementSensitivity.x * deltaTime;
            position.y += _moveDirection.y * _movementSensitivity.y * deltaTime;

            _transform.position = position;
        }
    }
}
