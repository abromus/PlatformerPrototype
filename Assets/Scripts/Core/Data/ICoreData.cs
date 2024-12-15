namespace PlatformerPrototype.Core.Data
{
    internal interface ICoreData : IDestroyable
    {
        public Configs.IConfigStorage ConfigStorage { get; }

        public Services.IServiceStorage ServiceStorage { get; }

        public Factories.IFactoryStorage FactoryStorage { get; }
    }
}
