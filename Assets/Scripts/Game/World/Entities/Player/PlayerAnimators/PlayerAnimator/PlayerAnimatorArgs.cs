namespace PlatformerPrototype.Game.World.Entities.Animators
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct PlayerAnimatorArgs
    {
        private readonly UnityEngine.Transform _player;
        private readonly IPlayerMovement _playerMovement;
        private readonly UnityEngine.Animator _animatorView;

        internal readonly UnityEngine.Transform Player => _player;

        internal readonly IPlayerMovement PlayerMovement => _playerMovement;

        internal readonly UnityEngine.Animator AnimatorView => _animatorView;

        internal PlayerAnimatorArgs(
            UnityEngine.Transform player,
            IPlayerMovement playerMovement,
            UnityEngine.Animator animatorView)
        {
            _player = player;
            _playerMovement = playerMovement;
            _animatorView = animatorView;
        }
    }
}
