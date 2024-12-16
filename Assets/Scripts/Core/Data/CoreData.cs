namespace PlatformerPrototype.Core.Data
{
    internal sealed class CoreData : ICoreData
    {
        private readonly Configs.IConfigStorage _configStorage;
        private readonly Services.IServiceStorage _serviceStorage;
        private readonly Factories.IFactoryStorage _factoryStorage;

        public Configs.IConfigStorage ConfigStorage => _configStorage;

        public Services.IServiceStorage ServiceStorage => _serviceStorage;

        public Factories.IFactoryStorage FactoryStorage => _factoryStorage;

        internal CoreData(
            UnityEngine.MonoBehaviour coroutineRunner,
            Configs.IConfigStorage configStorage,
            UnityEngine.Transform uiServiceContainer)
        {
            _configStorage = configStorage;
            _serviceStorage = new Services.ServiceStorage(coroutineRunner, this, uiServiceContainer);
            _factoryStorage = new Factories.FactoryStorage(configStorage);
        }

        public void Destroy()
        {
            _serviceStorage.Destroy();
        }
    }
}
