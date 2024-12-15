namespace PlatformerPrototype.Game.World.Entities
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct PlayerMovementArgs
    {
        private readonly IPlayerInput _playerInput;
        private readonly UnityEngine.Transform _transform;
        private readonly Configs.IPlayerConfig _playerConfig;

        internal readonly IPlayerInput PlayerInput => _playerInput;

        internal readonly UnityEngine.Transform Transform => _transform;

        internal readonly Configs.IPlayerConfig PlayerConfig => _playerConfig;

        internal PlayerMovementArgs(
            IPlayerInput playerInput,
            UnityEngine.Transform transform,
            Configs.IPlayerConfig playerConfig)
        {
            _playerInput = playerInput;
            _transform = transform;
            _playerConfig = playerConfig;
        }
    }
}
