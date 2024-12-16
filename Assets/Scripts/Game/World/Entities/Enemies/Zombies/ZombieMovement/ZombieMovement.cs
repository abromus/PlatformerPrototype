namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class ZombieMovement : IZombieMovement
    {
        private bool _isPaused;

        private readonly UnityEngine.Transform _player;
        private readonly UnityEngine.Transform _transform;
        private readonly float _speed;

        internal ZombieMovement(in ZombieMovementArgs args)
        {
            _player = args.Player;
            _transform = args.Transform;
            _speed = args.Speed;
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

        private void Move(float deltaTime)
        {
            if (_isPaused || _player == null)
                return;

            var position = _transform.position;
            var direction = _player.position.x < position.x ? Constants.Left : Constants.Right;
            position.x += _speed * deltaTime * direction;

            _transform.position = position;
        }
    }
}
