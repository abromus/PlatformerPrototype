namespace PlatformerPrototype.Core
{
    public interface IObjectPool<T> : IDestroyable where T : class, IPoolable
    {
        public T Get();

        public void Release(T @object);
    }
}
