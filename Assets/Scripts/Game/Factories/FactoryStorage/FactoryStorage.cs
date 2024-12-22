namespace PlatformerPrototype.Game.Factories
{
    internal sealed class FactoryStorage : Core.Factories.IFactoryStorage
    {
        private readonly Data.IGameData _gameData;
        private readonly Core.Configs.IConfigStorage _configStorage;
        private readonly System.Collections.Generic.Dictionary<System.Type, Core.Factories.IFactory> _factories;

        internal FactoryStorage(Data.IGameData gameData, Core.Configs.IConfigStorage configStorage)
        {
            _gameData = gameData;
            _configStorage = configStorage;

            var updater = _gameData.CoreData.ServiceStorage.GetService<Core.Services.IUpdaterService>();
            var uiFactories = _configStorage.GetConfig<Core.Configs.IUiFactoryConfig>().UiFactories;
            var dropFactory = InitDropFactory(uiFactories);
            var enemyFactory = InitEnemyFactory(uiFactories);
            var environmentFactory = InitEnvironmentFactory(uiFactories);
            var playerFactory = InitPlayerFactory(uiFactories);
            var projectileFactory = InitProjectileFactory(uiFactories, updater);
            var worldFactory = InitWorldFactory(uiFactories);

            _factories = new(8)
            {
                [typeof(IDropFactory)] = dropFactory,
                [typeof(IEnemyFactory)] = enemyFactory,
                [typeof(IEnvironmentFactory)] = environmentFactory,
                [typeof(IPlayerFactory)] = playerFactory,
                [typeof(IProjectileFactory)] = projectileFactory,
                [typeof(IWorldFactory)] = worldFactory,
            };
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public TFactory GetFactory<TFactory>() where TFactory : class, Core.Factories.IFactory
        {
            return _factories[typeof(TFactory)] as TFactory;
        }

        private IDropFactory InitDropFactory(Core.Factories.IUiFactory[] uiFactories)
        {
            var dropFactory = GetFactory<IDropFactory>(uiFactories);

            return dropFactory;
        }

        private IEnemyFactory InitEnemyFactory(Core.Factories.IUiFactory[] uiFactories)
        {
            var enemyFactory = GetFactory<IEnemyFactory>(uiFactories);

            return enemyFactory;
        }

        private IEnvironmentFactory InitEnvironmentFactory(Core.Factories.IUiFactory[] uiFactories)
        {
            var environmentFactory = GetFactory<IEnvironmentFactory>(uiFactories);

            return environmentFactory;
        }

        private IPlayerFactory InitPlayerFactory(Core.Factories.IUiFactory[] uiFactories)
        {
            var playerFactory = GetFactory<IPlayerFactory>(uiFactories);

            return playerFactory;
        }

        private IProjectileFactory InitProjectileFactory(Core.Factories.IUiFactory[] uiFactories, Core.Services.IUpdaterService updater)
        {
            var projectileFactory = GetFactory<IProjectileFactory>(uiFactories);
            projectileFactory.Init(updater);

            return projectileFactory;
        }

        private IWorldFactory InitWorldFactory(Core.Factories.IUiFactory[] uiFactories)
        {
            var worldFactory = GetFactory<IWorldFactory>(uiFactories);
            worldFactory.Init(_gameData);

            return worldFactory;
        }

        private TFactory GetFactory<TFactory>(Core.Factories.IUiFactory[] uiFactories) where TFactory : class, Core.Factories.IFactory
        {
            for (int i = 0; i < uiFactories.Length; i++)
            {
                var uiFactory = uiFactories[i];

                if (uiFactory is TFactory factory)
                    return factory;
            }

            return null;
        }
    }
}
