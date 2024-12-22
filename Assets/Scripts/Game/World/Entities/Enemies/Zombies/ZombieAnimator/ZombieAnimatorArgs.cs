namespace PlatformerPrototype.Game.World.Entities
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct ZombieAnimatorArgs
    {
        private readonly UnityEngine.Transform _zombieView;
        private readonly IZombieMovement _movement;
        private readonly UnityEngine.Animator _animatorView;

        internal readonly UnityEngine.Transform ZombieView => _zombieView;

        internal readonly IZombieMovement Movement => _movement;

        internal readonly UnityEngine.Animator AnimatorView => _animatorView;

        internal ZombieAnimatorArgs(
            UnityEngine.Transform zombieView,
            IZombieMovement movement,
            UnityEngine.Animator animatorView)
        {
            _zombieView = zombieView;
            _movement = movement;
            _animatorView = animatorView;
        }
    }
}
