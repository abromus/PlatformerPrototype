namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class ZombieAnimator : IZombieAnimator
    {
        private bool _isPaused;

        private readonly UnityEngine.Transform _zombieView;
        private readonly IZombieMovement _movement;
        private readonly UnityEngine.Animator _animatorView;

        internal ZombieAnimator(in ZombieAnimatorArgs args)
        {
            _zombieView = args.ZombieView;
            _movement = args.Movement;
            _animatorView = args.AnimatorView;

            UpdateMoveDirection();
        }

        public void LateTick(float deltaTime)
        {
            if (_isPaused)
                return;

            UpdateMoveDirection();
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
            _animatorView.enabled = isPaused == false;
        }

        private void UpdateMoveDirection()
        {
            var direction = _movement.Direction;
            var localScale = _zombieView.localScale;
            localScale.x = UnityEngine.Mathf.Abs(localScale.x) * direction;
            _zombieView.localScale = localScale;
        }
    }
}
