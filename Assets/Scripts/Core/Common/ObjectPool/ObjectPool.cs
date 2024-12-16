namespace PlatformerPrototype.Core
{
    public sealed class ObjectPool<T> : IObjectPool<T> where T : class, IPoolable
    {
        private readonly System.Collections.Generic.List<T> _objects;
        private readonly System.Func<T> _createFunc;

        public ObjectPool(System.Func<T> createFunc, int capacity = 16)
        {
            _objects = new(capacity);
            _createFunc = createFunc;
        }

        public T Get()
        {
            T @object;

            if (0 < _objects.Count)
            {
                var index = _objects.Count - 1;
                @object = _objects[index];

                _objects.RemoveAt(index);
            }
            else
            {
                @object = _createFunc();
            }

            return @object;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Release(T @object)
        {
            _objects.Add(@object);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            for (int i = 0; i < _objects.Count; i++)
                _objects[i].Destroy();

            _objects.Clear();
        }
    }
}
