namespace PlatformerPrototype.Game.World.Projectiles
{
    internal sealed class Projectile : BaseProjectile
    {
        [UnityEngine.SerializeField] private UnityEngine.Vector2 _size;
        [UnityEngine.SerializeField] private float _speed;
        [UnityEngine.SerializeField] private float _existenceTime;

        private Core.Services.IUpdaterService _updaterService;
        private UnityEngine.Vector3 _direction;
        private float _movingTime;
        private bool _isPaused;

        public override event System.Action<IProjectile> Destroyed;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Init(Core.Services.IUpdaterService updaterSevice)
        {
            _updaterService = updaterSevice;
        }

        public override void InitPosition(UnityEngine.Vector3 position, float direction)
        {
            transform.position = position;

            _direction.x = direction;
            _movingTime = 0f;

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

        public override void Clear()
        {
            Destroy();

            //отключить звук
        }

        public override void Destroy()
        {
            Unsubscribe();

            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            Destroyed?.Invoke(this);
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void Move(float deltaTime)
        {
            if (_isPaused)
                return;

            _movingTime += deltaTime;

            if (_movingTime < _existenceTime)
                transform.position += _speed * deltaTime * _direction;
            else
                Destroyed?.Invoke(this);
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

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEngine.Gizmos.color = UnityEngine.Color.green;
            UnityEngine.Gizmos.DrawWireCube(transform.position, _size);
        }
#endif
    }
}
