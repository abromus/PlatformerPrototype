namespace PlatformerPrototype.Core.Factories
{
    public interface IFactoryStorage
    {
        public TFactory GetFactory<TFactory>() where TFactory : class, IFactory;
    }
}
