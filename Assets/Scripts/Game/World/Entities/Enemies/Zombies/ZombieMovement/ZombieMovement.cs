namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class ZombieMovement : IZombieMovement
    {
        private bool _isPaused;
        private float _direction;

        private readonly UnityEngine.Transform _player;
        private readonly UnityEngine.Transform _transform;
        private readonly float _speed;

        public float Direction => _direction;

        internal ZombieMovement(in ZombieMovementArgs args)
        {
            _player = args.Player;
            _transform = args.Transform;
            _speed = args.Speed;

            CheckDirection();
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

        private void CheckDirection()
        {
            var position = _transform.position;
            _direction = _player.position.x < position.x ? Constants.Left : Constants.Right;
        }

        private void Move(float deltaTime)
        {
            if (_isPaused || _player == null)
                return;

            CheckDirection();

            var position = _transform.position;
            position.x += _speed * deltaTime * _direction;
            _transform.position = position;
        }
    }
}
