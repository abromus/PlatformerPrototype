namespace PlatformerPrototype.Game.World
{
    internal sealed class World : UnityEngine.MonoBehaviour, IWorld
    {
        [UnityEngine.SerializeField] private UnityEngine.Transform _enemyContainer;
        [UnityEngine.SerializeField] private UnityEngine.Transform _dropContainer;
        [UnityEngine.SerializeField] private UnityEngine.Transform _projectileContainer;

        private Data.IGameData _gameData;
        private Core.Services.IStateMachine _stateMachine;
        private Core.Services.ICameraService _cameraService;
        private Core.Services.IUpdaterService _updaterService;
        private Entities.IPlayer _player;
        private Entities.IEnemiesSpawner _enemiesSpawner;
        private DropStorages.IDropStorage _dropStorage;

        public void Init(Data.IGameData gameData, Core.Services.IStateMachine stateMachine)
        {
            _gameData = gameData;
            _stateMachine = stateMachine;

            var serviceStorage = _gameData.CoreData.ServiceStorage;
            _cameraService = serviceStorage.GetService<Core.Services.ICameraService>();
            _updaterService = serviceStorage.GetService<Core.Services.IUpdaterService>();

            var factoryStorage = _gameData.FactoryStorage;

            CreatePlayer(factoryStorage);
            InitEnemiesSpawner(factoryStorage);
            InitDropStorage(factoryStorage);

            _cameraService.AttachTo(_player.Transform);
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

            Subscribe();
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

        private void InitEnemiesSpawner(Core.Factories.IFactoryStorage factoryStorage)
        {
            var factory = factoryStorage.GetFactory<Factories.IEnemyFactory>();
            var config = _gameData.ConfigStorage.GetConfig<Configs.IEnemiesConfig>();
            var args = new Entities.EnemiesSpawnerArgs(_cameraService, factory, config, _player.Transform, _enemyContainer);

            _enemiesSpawner = new Entities.EnemiesSpawner(in args);
        }

        private void InitDropStorage(Core.Factories.IFactoryStorage factoryStorage)
        {
            var factory = factoryStorage.GetFactory<Factories.IDropFactory>();

            _dropStorage = new DropStorages.DropStorage(factory, _dropContainer);
        }

        private void GameOver()
        {
            _enemiesSpawner.Stop();
            _dropStorage.Stop();

            var args = new Services.GameStateArgs(this);

            _stateMachine.Enter<Services.GameOverState, Services.GameStateArgs>(args);
        }

        private void Subscribe()
        {
            _updaterService.AddUpdatable(this);
            _updaterService.AddFixedUpdatable(this);
            _updaterService.AddPausable(this);

            _player.Dead += OnPlayerDead;
            _enemiesSpawner.Dropped += OnDropped;
        }

        private void Unsubscribe()
        {
            if (_updaterService != null)
            {
                _updaterService.RemoveUpdatable(this);
                _updaterService.RemoveFixedUpdatable(this);
                _updaterService.RemovePausable(this);
            }

            if (_player != null)
                _player.Dead -= OnPlayerDead;

            if (_enemiesSpawner != null)
                _enemiesSpawner.Dropped -= OnDropped;
        }

        private void OnPlayerDead()
        {
            Unsubscribe();

            GameOver();
        }

        private void OnDropped(Configs.IDropConfig dropConfig, UnityEngine.Vector3 position)
        {
            _dropStorage.Drop(dropConfig, position);
        }
    }
}
