namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerMovement : IPlayerMovement
    {
        private bool _isPaused;
        private float _moveXDirection;
        private UnityEngine.Vector3 _lastPosition;

        private readonly IPlayerInput _playerInput;
        private readonly UnityEngine.Transform _transform;
        private readonly float _movementXSensitivity;

        public bool IsMoving => _lastPosition != _transform.position;

        internal PlayerMovement(in PlayerMovementArgs args)
        {
            _playerInput = args.PlayerInput;
            _transform = args.Transform;
            _movementXSensitivity = args.PlayerConfig.MovementXSensitivity;

            _lastPosition = _transform.position;
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

            _lastPosition = _transform.position;

            var position = _lastPosition;
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
