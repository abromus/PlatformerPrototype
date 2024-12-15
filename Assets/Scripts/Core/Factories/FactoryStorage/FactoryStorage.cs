namespace PlatformerPrototype.Core.Factories
{
    internal sealed class FactoryStorage : IFactoryStorage
    {
        private System.Collections.Generic.Dictionary<System.Type, IFactory> _factories;

        private readonly Configs.IConfigStorage _configStorage;

        internal FactoryStorage(Configs.IConfigStorage configStorage)
        {
            _configStorage = configStorage;

            var uiFactories = _configStorage.GetConfig<Configs.IUiFactoryConfig>().UiFactories;
            var gameSceneControllerFactory = InitGameSceneControllerFactory(uiFactories);

            _factories = new(8)
            {
                [typeof(IGameSceneControllerFactory)] = gameSceneControllerFactory,
            };
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public TFactory GetFactory<TFactory>() where TFactory : class, IFactory
        {
            return _factories[typeof(TFactory)] as TFactory;
        }

        private IGameSceneControllerFactory InitGameSceneControllerFactory(IUiFactory[] uiFactories)
        {
            var gameSceneControllerFactory = GetFactory<IGameSceneControllerFactory>(uiFactories);

            return gameSceneControllerFactory;
        }

        private TFactory GetFactory<TFactory>(IUiFactory[] uiFactories) where TFactory : class, IFactory
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
