namespace PlatformerPrototype.Game.Factories
{
    internal sealed class FactoryStorage : Core.Factories.IFactoryStorage
    {
        private System.Collections.Generic.Dictionary<System.Type, Core.Factories.IFactory> _factories;

        private readonly Core.Configs.IConfigStorage _configStorage;

        internal FactoryStorage(Core.Configs.IConfigStorage configStorage)
        {
            _configStorage = configStorage;

            var uiFactories = _configStorage.GetConfig<Core.Configs.IUiFactoryConfig>().UiFactories;
            var playerFactory = InitPlayerFactory(uiFactories);
            var worldFactory = InitWorldFactory(uiFactories);

            _factories = new(8)
            {
                [typeof(IPlayerFactory)] = playerFactory,
                [typeof(IWorldFactory)] = worldFactory,
            };
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public TFactory GetFactory<TFactory>() where TFactory : class, Core.Factories.IFactory
        {
            return _factories[typeof(TFactory)] as TFactory;
        }

        private IPlayerFactory InitPlayerFactory(Core.Factories.IUiFactory[] uiFactories)
        {
            var playerFactory = GetFactory<IPlayerFactory>(uiFactories);

            return playerFactory;
        }

        private IWorldFactory InitWorldFactory(Core.Factories.IUiFactory[] uiFactories)
        {
            var worldFactory = GetFactory<IWorldFactory>(uiFactories);

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
