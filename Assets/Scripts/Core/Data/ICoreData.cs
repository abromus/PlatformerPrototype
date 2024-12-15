namespace PlatformerPrototype.Core.Data
{
    public interface ICoreData : IDestroyable
    {
        public Configs.IConfigStorage ConfigStorage { get; }

        public Services.IServiceStorage ServiceStorage { get; }

        public Factories.IFactoryStorage FactoryStorage { get; }
    }
}
