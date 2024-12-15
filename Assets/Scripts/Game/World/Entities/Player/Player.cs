namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class Player : UnityEngine.MonoBehaviour, IPlayer
    {
        [UnityEngine.SerializeField] private Projectiles.Projectile _projectilePrefab;
        [UnityEngine.SerializeField] private UnityEngine.Animator _animator;

        private Data.IGameData _gameData;
        private UnityEngine.Transform _projectileContainer;
        private Core.Services.IUpdaterService _updaterService;
        private IPlayerInput _input;
        private IPlayerMovement _movement;
        private IPlayerShooting _shooting;

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

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            Unsubscribe();

            _shooting.Destroy();
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void InitModules()
        {
            var serviceStorage = _gameData.CoreData.ServiceStorage;

            _updaterService = serviceStorage.GetService<Core.Services.IUpdaterService>();

            var inputService = serviceStorage.GetService<Core.Services.IInputService>();
            var playerConfig = _gameData.ConfigStorage.GetConfig<Configs.IPlayerConfig>();
            
            _input = new PlayerInput(inputService);

            var playerMovementArgs = new PlayerMovementArgs(_input, transform, playerConfig);
            _movement = new PlayerMovement(in playerMovementArgs);

            var projectileFactory = _gameData.FactoryStorage.GetFactory<Factories.IProjectileFactory>();
            var playerShootingArgs = new PlayerShootingArgs(
                _input,
                transform,
                projectileFactory,
                _projectilePrefab,
                _projectileContainer,
                playerConfig);
            _shooting = new PlayerShooting(in playerShootingArgs);
            //var animator = new PlayerAnimator(_animator);
        }

        private void Subscribe()
        {
            _updaterService.AddUpdatable(this);
            _updaterService.AddFixedUpdatable(this);
            _updaterService.AddLateUpdatable(this);
            _updaterService.AddPausable(this);
        }

        private void Unsubscribe()
        {
            _updaterService.RemoveUpdatable(this);
            _updaterService.RemoveFixedUpdatable(this);
            _updaterService.RemoveLateUpdatable(this);
            _updaterService.RemovePausable(this);
        }
    }
}
