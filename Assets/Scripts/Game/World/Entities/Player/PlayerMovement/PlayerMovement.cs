namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerMovement : IPlayerMovement
    {
        private float _moveXDirection;
        private bool _isPaused;

        private readonly IPlayerInput _playerInput;
        private readonly UnityEngine.Transform _transform;
        private readonly float _movementXSensitivity;

        internal PlayerMovement(in PlayerMovementArgs args)
        {
            _playerInput = args.PlayerInput;
            _transform = args.Transform;
            _movementXSensitivity = args.PlayerConfig.MovementXSensitivity;
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
            _moveXDirection = _playerInput.MoveXDirection;
        }

        private void Move(float deltaTime)
        {
            if (_isPaused)
                return;

            var position = _transform.position;
            position.x += _moveXDirection * _movementXSensitivity * deltaTime;

            _transform.position = position;

            CheckDirection();
        }

        private void CheckDirection()
        {
            var localScale = _transform.localScale;

            if (localScale.x == Constants.Left && _moveXDirection == Constants.Right)
            {
                localScale.x = Constants.Right;
                _transform.localScale = localScale;
            }
            else if (localScale.x == Constants.Right && _moveXDirection == Constants.Left)
            {
                localScale.x = Constants.Left;
                _transform.localScale = localScale;
            }
        }
    }
}
