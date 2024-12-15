namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerMovement : IPlayerMovement
    {
        private UnityEngine.Vector2 _moveDirection;
        private bool _isPaused;

        private readonly IPlayerInput _playerInput;
        private readonly UnityEngine.Transform _transform;
        private readonly UnityEngine.Vector2 _movementSensitivity;

        internal PlayerMovement(in PlayerMovementArgs args)
        {
            _playerInput = args.PlayerInput;
            _transform = args.Transform;
            _movementSensitivity = args.PlayerConfig.MovementSensitivity;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick(float deltaTime)
        {
            CheckInput();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void FixedTick(float deltaTime)
        {
            Move(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
