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
        private DeathReason _deathReason;

        private IZombieMovement _movement;
        private IZombieHealth _health;
        private IZombieAnimator _animator;
        private IEnemyAudio _audio;

        public override int Index => _index;

        public override UnityEngine.Vector3 Position => transform.position;

        public override UnityEngine.Vector2 Size => _size;

        public override float Damage => _damage;

        public override Configs.IDropConfig DropConfig => _dropConfig;

        public override DeathReason DeathReason => _deathReason;

        public override event System.Action<IEnemy> Died;

        public override void Init(in EnemyArgs args)
        {
            var info = args.Info;

            _index = info.Index;
            _damage = info.Damage;
            _dropConfig = info.DropConfig;

            InitModules(args.AudioService, args.Player, in info);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void InitPosition(in UnityEngine.Vector3 position)
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
            _deathReason = DeathReason.None;
            _rigidbody.simulated = true;
            _health.SetActive(true);
            _audio.PlayRunningClip();

            gameObject.SetActive(true);

            Subscribe();
        }

        public override void Deactivate()
        {
            _isDead = true;
            _rigidbody.simulated = false;
            _health.SetActive(false);
            _audio.StopLoopClips();

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
                Die(DeathReason.Player);
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void InitModules(Services.IAudioService audioService, UnityEngine.Transform player, in Configs.EnemyInfo info)
        {
            var movementArgs = new ZombieMovementArgs(player, transform, info.Speed);
            _movement = new ZombieMovement(in movementArgs);

            var healthArgs = new ZombieHealthArgs(_healthView, info.Hp);
            _health = new ZombieHealth(in healthArgs);

            var animatorArgs = new ZombieAnimatorArgs(_view, _movement, _animatorView);
            _animator = new ZombieAnimator(in animatorArgs);

            var audioArgs = new ZombieAudioArgs(audioService, info.RunningClip, info.DeathClip);
            _audio = new ZombieAudio(in audioArgs);
        }

        private void Die(DeathReason deathReason)
        {
            if (_isDead)
                return;

            _isDead = true;
            _deathReason = deathReason;
            _rigidbody.simulated = false;

            _audio.PlayDeathClip();

            Died?.Invoke(this);
        }

        private void Subscribe()
        {
            _health.Died += OnHealthEnded;
        }

        private void Unsubscribe()
        {
            _health.Died -= OnHealthEnded;
        }

        private void OnHealthEnded()
        {
            Die(DeathReason.HealthEnded);
        }
    }
}
