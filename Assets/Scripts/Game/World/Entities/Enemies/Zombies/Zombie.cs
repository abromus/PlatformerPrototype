namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class Zombie : BaseEnemy
    {
        [UnityEngine.SerializeField] private UnityEngine.Rigidbody2D _rigidbody;
        [UnityEngine.SerializeField] private UnityEngine.Vector2 _size;

        private Core.Services.IUpdaterService _updaterService;
        private UnityEngine.Transform _player;

        private int _index;
        private float _speed;
        private bool _isPaused;

        public override int Index => _index;

        public override UnityEngine.Vector2 Size => _size;

        public override event System.Action<IEnemy> Dead;

        public override void Init(Core.Services.IUpdaterService updaterSevice, float speed)
        {
            _updaterService = updaterSevice;
            _speed = speed;
        }

        public override void InitPosition(UnityEngine.Vector3 position, UnityEngine.Transform player)
        {
            transform.position = position;

            _player = player;
            _rigidbody.simulated = true;

            gameObject.SetActive(true);

            Subscribe();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Clear()
        {
            Destroy();

            //отключить звук
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void SetIndex(int index)
        {
            _index = index;
        }

        public override void Destroy()
        {
            Unsubscribe();

            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            _rigidbody.simulated = false;

            Dead?.Invoke(this);
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void Move(float deltaTime)
        {
            if (_isPaused || _player == null)
                return;

            var position = transform.position;
            var direction = _player.position.x < position.x ? Constants.Left : Constants.Right;
            position.x += _speed * deltaTime * direction;

            transform.position = position;
        }

        private void Subscribe()
        {
            _updaterService.AddUpdatable(this);
            _updaterService.AddPausable(this);
        }

        private void Unsubscribe()
        {
            if (_updaterService != null)
            {
                _updaterService.RemoveUpdatable(this);
                _updaterService.RemovePausable(this);
            }
        }
    }
}
