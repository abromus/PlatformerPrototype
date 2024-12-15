namespace PlatformerPrototype.Core.Factories
{
    internal interface IFactoryStorage
    {
        public TFactory GetFactory<TFactory>() where TFactory : class, IFactory;
    }
}
