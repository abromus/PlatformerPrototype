namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class Player : UnityEngine.MonoBehaviour, IPlayer
    {
        [UnityEngine.SerializeField] private Projectiles.Projectile _projectilePrefab;
        [UnityEngine.SerializeField] private UnityEngine.Animator _animator;

        private Data.IGameData _gameData;
        private UnityEngine.Transform _projectileContainer;

        private IPlayerInput _input;
        private IPlayerMovement _movement;
        private IPlayerShooting _shooting;
        private IPlayerHealth _health;

        public UnityEngine.Transform Transform => transform;

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

        public void SetPause(bool isPaused)
        {
            _input.SetPause(isPaused);
            _movement.SetPause(isPaused);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetParent(UnityEngine.Transform parent)
        {
            transform.SetParent(parent);
        }

        public void Restart()
        {
            transform.position = UnityEngine.Vector3.zero;

            var localScale = transform.localScale;
            localScale.x = 1f;
            transform.localScale = localScale;

            _shooting.Restart();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            Unsubscribe();

            _shooting.Destroy();
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            if (collision.TryGetComponent<IEnemy>(out var damagable) == false)
                return;

            _health.Change(-damagable.Damage);
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void InitModules()
        {
            var inputService = _gameData.CoreData.ServiceStorage.GetService<Core.Services.IInputService>();
            var playerConfig = _gameData.ConfigStorage.GetConfig<Configs.IPlayerConfig>();

            _input = new PlayerInput(inputService);

            var movementArgs = new PlayerMovementArgs(_input, transform, playerConfig);
            _movement = new PlayerMovement(in movementArgs);

            var projectileFactory = _gameData.FactoryStorage.GetFactory<Factories.IProjectileFactory>();
            var shootingArgs = new PlayerShootingArgs(
                _input,
                transform,
                projectileFactory,
                _projectilePrefab,
                _projectileContainer,
                playerConfig);
            _shooting = new PlayerShooting(in shootingArgs);

            _health = new PlayerHealth(playerConfig.Hp);

            //var animator = new PlayerAnimator(_animator);
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
            Dead?.Invoke();
        }
    }
}
