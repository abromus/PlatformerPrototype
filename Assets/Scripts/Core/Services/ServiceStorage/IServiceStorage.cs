namespace PlatformerPrototype.Core.Services
{
    internal interface IServiceStorage : IDestroyable
    {
        public TService GetService<TService>() where TService : class, IService;
    }
}
