namespace PlatformerPrototype.Core.Services
{
    public interface IServiceStorage : IDestroyable
    {
        public TService GetService<TService>() where TService : class, IService;
    }
}
