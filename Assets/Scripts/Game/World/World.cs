namespace PlatformerPrototype.Game.World
{
    internal sealed class World : UnityEngine.MonoBehaviour, IWorld
    {
        [UnityEngine.SerializeField] private UnityEngine.Transform _backgroundCamera;
        [UnityEngine.SerializeField] private UnityEngine.Transform _backgroundsContainer;
        [UnityEngine.SerializeField] private Background _backgroundPrefab;
        [UnityEngine.SerializeField] private float _backgroundOffsetSpeed;
        [UnityEngine.SerializeField] private UnityEngine.Transform _chunksContainer;
        [UnityEngine.SerializeField] private Chunk _chunkPrefab;
        [UnityEngine.SerializeField] private UnityEngine.Transform _earth;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.Transform _enemyContainer;
        [UnityEngine.SerializeField] private UnityEngine.Transform _dropContainer;
        [UnityEngine.SerializeField] private UnityEngine.Transform _projectileContainer;

        private Data.IGameData _gameData;
        private Core.Services.IStateMachine _stateMachine;
        private Core.Services.ICameraService _cameraService;
        private Services.IScreenSystemService _screenSystemService;
        private Core.Services.IUpdaterService _updaterService;

        private Entities.IPlayer _player;
        private Entities.IEnemiesSpawner _enemiesSpawner;
        private IEnvironmentStorage _environmentStorage;
        private DropStorages.IDropStorage _dropStorage;
        private Services.IScreenArgs _mainScreenArgs;

        public void Init(Data.IGameData gameData, Core.Services.IStateMachine stateMachine)
        {
            _gameData = gameData;
            _stateMachine = stateMachine;

            var coreServiceStorage = _gameData.CoreData.ServiceStorage;
            var serviceStorage = _gameData.ServiceStorage;
            _cameraService = coreServiceStorage.GetService<Core.Services.ICameraService>();
            _updaterService = coreServiceStorage.GetService<Core.Services.IUpdaterService>();
            _screenSystemService = serviceStorage.GetService<Services.IScreenSystemService>();

            var audioService = serviceStorage.GetService<Services.IAudioService>();
            var factoryStorage = _gameData.FactoryStorage;

            CreatePlayer(factoryStorage);
            InitBackground();
            InitEnvironmentStorage(factoryStorage);
            InitEnemiesSpawner(audioService, factoryStorage);
            InitDropStorage(audioService, factoryStorage);

            _cameraService.AttachTo(_player.Transform);

            _mainScreenArgs = new Services.MainScreenArgs(audioService, _player);

            Subscribe();
        }

        public void Tick(float deltaTime)
        {
            _player.Tick(deltaTime);
            _enemiesSpawner.Tick(deltaTime);
        }

        public void FixedTick(float deltaTime)
        {
            _player.FixedTick(deltaTime);
            _enemiesSpawner.FixedTick(deltaTime);
        }

        public void LateTick(float deltaTime)
        {
            _player.LateTick(deltaTime);
            _enemiesSpawner.LateTick(deltaTime);
            _environmentStorage.LateTick(deltaTime);
        }

        public void SetPause(bool isPaused)
        {
            _player.SetPause(isPaused);
            _enemiesSpawner.SetPause(isPaused);
        }

        public void Restart()
        {
            _player.Restart();
            _enemiesSpawner.Restart();
            _dropStorage.Restart();
            _environmentStorage.Restart();

            ShowMainScreen();
            SubscribeOnUpdaterService();
        }

        public void Destroy()
        {
            Unsubscribe();

            _player?.Destroy();
            _gameData.Destroy();
        }

        private void CreatePlayer(Core.Factories.IFactoryStorage factoryStorage)
        {
            var factory = factoryStorage.GetFactory<Factories.IPlayerFactory>();

            _player = factory.Create();
            _player.Init(_gameData, _projectileContainer);
            _player.SetParent(transform);
        }

        private void InitBackground()
        {
            _backgroundCamera.SetParent(_cameraService.CameraTransform);
            _earth.SetParent(_player.Transform);

            var position = _backgroundCamera.transform.localPosition;
            position.y = 0f;
            _backgroundCamera.transform.localPosition = position;
        }

        private void InitEnvironmentStorage(Core.Factories.IFactoryStorage factoryStorage)
        {
            var factory = factoryStorage.GetFactory<Factories.IEnvironmentFactory>();
            var args = new EnvironmentStorageArgs(
                factory,
                _player.Transform,
                _backgroundsContainer,
                _backgroundPrefab,
                _backgroundOffsetSpeed,
                _chunksContainer,
                _chunkPrefab);

            _environmentStorage = new EnvironmentStorage(in args);
        }

        private void InitEnemiesSpawner(Services.IAudioService audioService, Core.Factories.IFactoryStorage factoryStorage)
        {
            var factory = factoryStorage.GetFactory<Factories.IEnemyFactory>();
            var config = _gameData.ConfigStorage.GetConfig<Configs.IEnemiesConfig>();
            var args = new Entities.EnemiesSpawnerArgs(
                _cameraService,
                audioService,
                factory,
                config,
                _player.Transform,
                _enemyContainer);

            _enemiesSpawner = new Entities.EnemiesSpawner(in args);
        }

        private void InitDropStorage(Services.IAudioService audioService, Core.Factories.IFactoryStorage factoryStorage)
        {
            var factory = factoryStorage.GetFactory<Factories.IDropFactory>();
            var args = new DropStorages.DropStorageArgs(audioService, factory, _dropContainer);
            _dropStorage = new DropStorages.DropStorage(in args);
        }

        private void GameOver()
        {
            _player.Stop();
            _enemiesSpawner.Stop();

            _stateMachine.Enter<Services.GameOverState>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void ShowMainScreen()
        {
            _screenSystemService.Show(Configs.ScreenType.Main, in _mainScreenArgs);
        }

        private void Subscribe()
        {
            _gameData.Restarted += OnRestarted;

            _player.Dead += OnPlayerDead;
            _enemiesSpawner.Dropped += OnDropped;
        }

        private void Unsubscribe()
        {
            if (_gameData != null)
                _gameData.Restarted -= OnRestarted;

            if (_player != null)
                _player.Dead -= OnPlayerDead;

            if (_enemiesSpawner != null)
                _enemiesSpawner.Dropped -= OnDropped;
        }

        private void SubscribeOnUpdaterService()
        {
            _updaterService.AddUpdatable(this);
            _updaterService.AddFixedUpdatable(this);
            _updaterService.AddLateUpdatable(this);
            _updaterService.AddPausable(this);
        }

        private void UnubscribeOnUpdaterService()
        {
            if (_updaterService != null)
            {
                _updaterService.RemoveUpdatable(this);
                _updaterService.RemoveFixedUpdatable(this);
                _updaterService.RemoveLateUpdatable(this);
                _updaterService.RemovePausable(this);
            }
        }

        private void OnRestarted()
        {
            var args = new Services.GameStateArgs(this);

            _stateMachine.Enter<Services.GameRestartState, Services.GameStateArgs>(in args);
        }

        private void OnPlayerDead()
        {
            UnubscribeOnUpdaterService();

            GameOver();
        }

        private void OnDropped(Configs.IDropConfig dropConfig, UnityEngine.Vector3 position)
        {
            _dropStorage.Drop(dropConfig, in position);
        }
    }
}
