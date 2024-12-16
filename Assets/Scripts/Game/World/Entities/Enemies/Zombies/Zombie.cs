namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class Zombie : BaseEnemy
    {
        [UnityEngine.SerializeField] private Health.HealthView _healthView;
        [UnityEngine.SerializeField] private UnityEngine.Rigidbody2D _rigidbody;
        [UnityEngine.SerializeField] private UnityEngine.Vector2 _size;

        private Core.Services.IUpdaterService _updaterService;

        private int _index;

        private IZombieMovement _movement;
        private IZombieHealth _health;

        public override int Index => _index;

        public override UnityEngine.Vector2 Size => _size;

        public override event System.Action<IEnemy> Dead;

        public override void Init(in EnemyArgs args)
        {
            _updaterService = args.UpdaterService;

            InitModules(in args);
        }

        public override void InitPosition(UnityEngine.Vector3 position)
        {
            transform.position = position;
        }

        public override void InitHp()
        {
            _health.InitHp();
        }

        public override void Activate()
        {
            _rigidbody.simulated = true;
            _health.SetActive(true);

            gameObject.SetActive(true);

            Subscribe();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void FixedTick(float deltaTime)
        {
            _movement.FixedTick(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void SetPause(bool isPaused)
        {
            _movement.SetPause(isPaused);
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

            _health.SetActive(false);

            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            if (collision.TryGetComponent<IDamagable>(out var damagable) == false)
                return;

            _health.Change(-damagable.Damage);
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void InitModules(in EnemyArgs args)
        {
            var movementArgs = new ZombieMovementArgs(args.Player, transform, args.Speed);
            _movement = new ZombieMovement(in movementArgs);

            var healthArgs = new ZombieHealthArgs(_healthView, args.Hp);
            _health = new ZombieHealth(in healthArgs);
        }

        private void Subscribe()
        {
            _updaterService.AddFixedUpdatable(this);
            _updaterService.AddPausable(this);

            _health.Dead += OnDead;
        }

        private void Unsubscribe()
        {
            if (_updaterService != null)
            {
                _updaterService.RemoveFixedUpdatable(this);
                _updaterService.RemovePausable(this);
            }

            _health.Dead -= OnDead;
        }

        private void OnDead()
        {
            _rigidbody.simulated = false;

            Dead?.Invoke(this);
        }
    }
}
