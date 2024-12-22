namespace PlatformerPrototype.Game.World.Entities.Animators
{
    internal sealed class PlayerAnimator : IPlayerAnimator
    {
        private bool _isPaused;
        private float _lastDirection;

        private readonly UnityEngine.Transform _player;
        private readonly IPlayerMovement _playerMovement;
        private readonly UnityEngine.Animator _animatorView;

        internal PlayerAnimator(UnityEngine.Transform player, IPlayerMovement playerMovement, UnityEngine.Animator animatorView)
        {
            _player = player;
            _playerMovement = playerMovement;
            _animatorView = animatorView;

            _lastDirection = _player.localScale.x;

            UpdateMoveDirection(_lastDirection);
        }

        public void LateTick(float deltaTime)
        {
            if (_isPaused)
                return;

            CheckMoveDirection();
            UpdateState();
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
            _animatorView.enabled = isPaused == false;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Stop()
        {
            _animatorView.SetBool(Keys.IsMoving, false);
        }

        private void CheckMoveDirection()
        {
            if (_playerMovement.IsMoving == false)
                return;

            var direction = _player.localScale.x;

            if (_lastDirection == Constants.Left && direction == Constants.Right)
                UpdateMoveDirection(Constants.Right);
            else if (_lastDirection == Constants.Right && direction == Constants.Left)
                UpdateMoveDirection(Constants.Left);
        }

        private void UpdateMoveDirection(float direction)
        {
            _lastDirection = _player.localScale.x;

            var localScale = _player.localScale;
            localScale.x = direction;
            _player.localScale = localScale;
        }

        private void UpdateState()
        {
            if (_playerMovement.IsMoving)
                _animatorView.SetBool(Keys.IsMoving, true);
            else
                _animatorView.SetBool(Keys.IsMoving, false);
        }

        private sealed class Keys
        {
            internal const string IsMoving = "IsMoving";
        }
    }
}
