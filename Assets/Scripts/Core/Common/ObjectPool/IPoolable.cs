namespace PlatformerPrototype.Core
{
    public interface IPoolable : IDestroyable
    {
        public void Activate();

        public void Clear();
    }
}
