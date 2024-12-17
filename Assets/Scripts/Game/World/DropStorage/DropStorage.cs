namespace PlatformerPrototype.Game.DropStorages
{
    internal sealed class DropStorage : IDropStorage
    {
        private Configs.IDropConfig _currentDropConfig;

        private readonly Factories.IDropFactory _factory;
        private readonly UnityEngine.Transform _container;

        private readonly Core.IObjectPool<World.Drops.IDrop> _pool;
        private readonly System.Collections.Generic.List<World.Drops.IDrop> _drops = new(64);

        internal DropStorage(Factories.IDropFactory factory, UnityEngine.Transform container)
        {
            _factory = factory;
            _container = container;

            _pool = new Core.ObjectPool<World.Drops.IDrop>(CreateDrop);
        }

        public void Drop(Configs.IDropConfig dropConfig, UnityEngine.Vector3 position)
        {
            _currentDropConfig = dropConfig;

            var drop = _pool.Get();
            drop.InitPosition(position);
            drop.Activate();
            drop.Destroyed += OnDropDestroyed;
        }

        private World.Drops.IDrop CreateDrop()
        {
            var drop = _factory.Create(_currentDropConfig, _container);

            _drops.Add(drop);

            return drop;
        }

        public void Restart()
        {
            for (int i = 0; i < _drops.Count; i++)
                _pool.Release(_drops[i]);
        }

        public void Stop()
        {
            for (int i = 0; i < _drops.Count; i++)
            {
                var drop = _drops[i];
                drop.Destroyed -= OnDropDestroyed;
                drop.Clear();

                _pool.Release(drop);
            }
        }

        public void Destroy()
        {
            for (int i = 0; i < _drops.Count; i++)
                _pool.Release(_drops[i]);

            _drops.Clear();
            _pool.Destroy();
        }

        private void OnDropDestroyed(World.Drops.IDrop drop)
        {
            drop.Destroyed -= OnDropDestroyed;
            drop.Clear();

            _pool.Release(drop);
        }
    }
}
