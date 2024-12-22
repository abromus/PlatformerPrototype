namespace PlatformerPrototype.Game.World.Entities.Animators
{
    internal sealed class PlayerShootingAnimator : IPlayerShootingAnimator
    {
        private bool _isPaused;
        private int _projectileCount;

        private readonly UnityEngine.Animator _animatorView;

        internal PlayerShootingAnimator(UnityEngine.Animator animatorView)
        {
            _animatorView = animatorView;
        }

        public void LateTick(float deltaTime)
        {
            if (_isPaused)
                return;

            UpdateState();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Animate()
        {
            ++_projectileCount;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Stop()
        {
            _animatorView.SetBool(Keys.IsShooting, false);
        }

        private void UpdateState()
        {
            if (0 < _projectileCount)
            {
                _animatorView.SetBool(Keys.IsShooting, true);

                --_projectileCount;
            }
            else
            {
                _animatorView.SetBool(Keys.IsShooting, false);
            }
        }

        private sealed class Keys
        {
            internal const string IsShooting = "IsShooting";
        }
    }
}
