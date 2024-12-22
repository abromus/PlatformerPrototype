namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class Player : UnityEngine.MonoBehaviour, IPlayer
    {
        [UnityEngine.SerializeField] private UnityEngine.Transform _weaponTransform;
        [UnityEngine.SerializeField] private UnityEngine.Animator _animatorView;
        [UnityEngine.SerializeField] private UnityEngine.Animator _shootingAnimatorView;

        private Data.IGameData _gameData;
        private UnityEngine.Transform _projectileContainer;

        private IPlayerInput _input;
        private IPlayerMovement _movement;
        private IPlayerShooting _shooting;
        private IPlayerHealth _health;
        private IPlayerDropConsumer _dropConsumer;
        private IPlayerAnimator _animator;

        public UnityEngine.Transform Transform => transform;

        public int CurrentAmmo => _shooting.CurrentAmmo;

        public event System.Action AmmoChanged;

        public event System.Action Dead;

        public void Init(Data.IGameData gameData, UnityEngine.Transform projectileContainer)
        {
            _gameData = gameData;
            _projectileContainer = projectileContainer;

            InitModules();
            Subscribe();
        }

        public void Tick(float deltaTime)
        {
            _input.Tick(deltaTime);
            _movement.Tick(deltaTime);
            _shooting.Tick(deltaTime);
        }

        public void FixedTick(float deltaTime)
        {
            _movement.FixedTick(deltaTime);
            _shooting.FixedTick(deltaTime);
        }

        public void LateTick(float deltaTime)
        {
            _animator.LateTick(deltaTime);
            _shooting.LateTick(deltaTime);
        }

        public void SetPause(bool isPaused)
        {
            _input.SetPause(isPaused);
            _movement.SetPause(isPaused);
            _shooting.SetPause(isPaused);
            _animator.SetPause(isPaused);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetParent(UnityEngine.Transform parent)
        {
            transform.SetParent(parent);
        }

        public void Stop()
        {
            _animator.Stop();
            _shooting.StopAnimation();
        }

        public void Restart()
        {
            transform.position = UnityEngine.Vector3.zero;

            var localScale = transform.localScale;
            localScale.x = 1f;
            transform.localScale = localScale;

            _shooting.Restart();
        }

        public void Destroy()
        {
            Unsubscribe();

            _shooting.Destroy();
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            if (collision.TryGetComponent<Drops.IDrop>(out var drop))
            {
                _dropConsumer.Apply(drop);

                drop.Apply();
            }
            else if (collision.TryGetComponent<IEnemy>(out var damagable))
            {
                _health.Change(-damagable.Damage);
            }
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void InitModules()
        {
            var eventSystemService = _gameData.ServiceStorage.GetService<Services.IEventSystemService>();
            var inputService = _gameData.CoreData.ServiceStorage.GetService<Core.Services.IInputService>();
            var playerConfig = _gameData.ConfigStorage.GetConfig<Configs.IPlayerConfig>();

            _input = new PlayerInput(eventSystemService, inputService);

            var movementArgs = new PlayerMovementArgs(_input, transform, playerConfig);
            _movement = new PlayerMovement(in movementArgs);

            var projectileFactory = _gameData.FactoryStorage.GetFactory<Factories.IProjectileFactory>();
            var shootingArgs = new PlayerShootingArgs(
                _input,
                transform,
                _weaponTransform,
                projectileFactory,
                _projectileContainer,
                playerConfig,
                _shootingAnimatorView);
            _shooting = new PlayerShooting(in shootingArgs);

            _health = new PlayerHealth(playerConfig.Hp);

            _dropConsumer = new PlayerDropConsumer(_shooting);

            _animator = new Animators.PlayerAnimator(transform, _movement, _animatorView);
        }

        private void Subscribe()
        {
            _health.Dead += OnDead;
            _shooting.AmmoChanged += OnAmmoChanged;
            _shooting.AmmoOut += OnAmmoOut;
        }

        private void Unsubscribe()
        {
            _health.Dead -= OnDead;
            _shooting.AmmoChanged -= OnAmmoChanged;
            _shooting.AmmoOut -= OnAmmoOut;
        }

        private void OnDead()
        {
            Dead?.Invoke();
        }

        private void OnAmmoChanged()
        {
            AmmoChanged?.Invoke();
        }

        private void OnAmmoOut()
        {
            Dead?.Invoke();
        }
    }
}
