namespace PlatformerPrototype.Game.Data
{
    internal sealed class GameData : IGameData
    {
        private readonly Core.Data.ICoreData _coreData;
        private readonly Core.Configs.IConfigStorage _configStorage;
        private readonly Core.Services.IServiceStorage _serviceStorage;
        private readonly Core.Factories.IFactoryStorage _factoryStorage;

        public Core.Data.ICoreData CoreData => _coreData;

        public Core.Configs.IConfigStorage ConfigStorage => _configStorage;

        public Core.Services.IServiceStorage ServiceStorage => _serviceStorage;

        public Core.Factories.IFactoryStorage FactoryStorage => _factoryStorage;

        internal GameData(Core.Data.ICoreData coreData, Core.Configs.IConfigStorage configStorage)
        {
            _coreData = coreData;
            _configStorage = configStorage;
            _serviceStorage = new Services.ServiceStorage(this);
            _factoryStorage = new Factories.FactoryStorage(this, configStorage);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            _serviceStorage.Destroy();
        }
    }
}