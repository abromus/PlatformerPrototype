namespace PlatformerPrototype.Core
{
    public interface IPoolable : IDestroyable
    {
        public void Activate();

        public void Deactivate();

        public void Clear();
    }
}
