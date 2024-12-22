namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class Zombie : BaseEnemy
    {
        [UnityEngine.SerializeField] private UnityEngine.Transform _view;
        [UnityEngine.SerializeField] private Health.HealthView _healthView;
        [UnityEngine.SerializeField] private UnityEngine.Rigidbody2D _rigidbody;
        [UnityEngine.SerializeField] private UnityEngine.Animator _animatorView;
        [UnityEngine.SerializeField] private UnityEngine.Vector2 _size;

        private int _index;
        private float _damage;
        private bool _isDead;
        private Configs.IDropConfig _dropConfig;

        private IZombieMovement _movement;
        private IZombieHealth _health;
        private IZombieAnimator _animator;

        public override int Index => _index;

        public override UnityEngine.Vector3 Position => transform.position;

        public override UnityEngine.Vector2 Size => _size;

        public override float Damage => _damage;

        public override Configs.IDropConfig DropConfig => _dropConfig;

        public override event System.Action<IEnemy> Dead;

        public override void Init(in EnemyArgs args)
        {
            _damage = args.Damage;
            _dropConfig = args.DropConfig;

            InitModules(in args);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void InitPosition(UnityEngine.Vector3 position)
        {
            transform.position = position;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void InitHp()
        {
            _health.InitHp();
        }

        public override void Activate()
        {
            _isDead = false;
            _rigidbody.simulated = true;
            _health.SetActive(true);

            gameObject.SetActive(true);

            Subscribe();
        }

        public override void Deactivate()
        {
            _isDead = true;
            _rigidbody.simulated = false;
            _health.SetActive(false);

            gameObject.SetActive(false);

            Unsubscribe();
        }    

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void FixedTick(float deltaTime)
        {
            _movement.FixedTick(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void LateTick(float deltaTime)
        {
            _animator.LateTick(deltaTime);
        }

        public override void SetPause(bool isPaused)
        {
            _movement.SetPause(isPaused);
            _animator.SetPause(isPaused);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Clear()
        {
            Deactivate();

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
            if (collision.TryGetComponent<Projectiles.IProjectile>(out var damagable))
                _health.Change(-damagable.Damage);
            else if (collision.TryGetComponent<IPlayer>(out _))
                OnDead();
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

            var animatorArgs = new ZombieAnimatorArgs(_view, _movement, _animatorView);
            _animator = new ZombieAnimator(in animatorArgs);
        }

        private void Subscribe()
        {
            _health.Dead += OnDead;
        }

        private void Unsubscribe()
        {
            _health.Dead -= OnDead;
        }

        private void OnDead()
        {
            if (_isDead)
                return;

            _isDead = true;
            _rigidbody.simulated = false;

            Dead?.Invoke(this);
        }
    }
}
