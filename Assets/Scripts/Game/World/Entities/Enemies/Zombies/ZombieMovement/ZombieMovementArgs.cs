namespace PlatformerPrototype.Game.World.Entities
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct ZombieMovementArgs
    {
        private readonly UnityEngine.Transform _player;
        private readonly UnityEngine.Transform _transform;
        private readonly float _speed;

        internal readonly UnityEngine.Transform Player => _player;

        internal readonly UnityEngine.Transform Transform => _transform;

        internal readonly float Speed => _speed;

        internal ZombieMovementArgs(
            UnityEngine.Transform player,
            UnityEngine.Transform transform,
            float speed)
        {
            _player = player;
            _transform = transform;
            _speed = speed;
        }
    }
}
