namespace PlatformerPrototype.Game.World
{
    internal sealed class World : UnityEngine.MonoBehaviour, IWorld
    {
        [UnityEngine.SerializeField] private Entities.EnemiesSpawner _enemiesSpawner;
        [UnityEngine.SerializeField] private UnityEngine.Transform _projectileContainer;

        private Data.IGameData _gameData;
        private Core.Services.ICameraService _cameraService;
        private Entities.IPlayer _player;

        public void Init(Data.IGameData gameData)
        {
            _gameData = gameData;
            _cameraService = _gameData.CoreData.ServiceStorage.GetService<Core.Services.ICameraService>();

            var factoryStorage = _gameData.FactoryStorage;

            CreatePlayer(factoryStorage);
            InitEnemiesSpawner(factoryStorage);

            _cameraService.AttachTo(_player.Transform);
        }

        public void Restart()
        {

        }

        public void Destroy()
        {
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
            var updaterService = _gameData.CoreData.ServiceStorage.GetService<Core.Services.IUpdaterService>();
            var factory = factoryStorage.GetFactory<Factories.IEnemyFactory>();
            var config = _gameData.ConfigStorage.GetConfig<Configs.IEnemiesConfig>();
            var args = new Entities.EnemiesSpawnerArgs(_cameraService, updaterService, factory, config, _player.Transform);

            _enemiesSpawner.Init(in args);
        }
    }
}
