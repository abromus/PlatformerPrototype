﻿namespace PlatformerPrototype.Game.DropStorages
{
    internal sealed class DropStorage : IDropStorage
    {
        private Configs.IDropConfig _currentDropConfig;

        private readonly Services.IAudioService _audioService;
        private readonly Factories.IDropFactory _factory;
        private readonly UnityEngine.Transform _container;

        private readonly Core.IObjectPool<World.Drops.IDrop> _pool;
        private readonly System.Collections.Generic.List<World.Drops.IDrop> _drops = new(64);

        internal DropStorage(in DropStorageArgs args)
        {
            _audioService = args.AudioService;
            _factory = args.Factory;
            _container = args.Container;

            _pool = new Core.ObjectPool<World.Drops.IDrop>(CreateDrop);
        }

        public void Drop(Configs.IDropConfig dropConfig, in UnityEngine.Vector3 position)
        {
            _currentDropConfig = dropConfig;

            var drop = _pool.Get();
            drop.InitPosition(in position);
            drop.Activate();
            drop.Destroyed += OnDropDestroyed;
        }

        public void Restart()
        {
            for (int i = 0; i < _drops.Count; i++)
            {
                var drop = _drops[i];
                drop.Destroyed -= OnDropDestroyed;
                drop.Deactivate();
                drop.Clear();

                _pool.Release(drop);
            }
        }

        public void Destroy()
        {
            for (int i = 0; i < _drops.Count; i++)
            {
                var drop = _drops[i];
                drop.Destroyed -= OnDropDestroyed;
                drop.Deactivate();
                drop.Clear();

                _pool.Release(drop);
            }

            _drops.Clear();
            _pool.Destroy();
        }

        private World.Drops.IDrop CreateDrop()
        {
            var drop = _factory.Create(_audioService, _currentDropConfig, _container);

            _drops.Add(drop);

            return drop;
        }

        private void OnDropDestroyed(World.Drops.IDrop drop)
        {
            drop.Destroyed -= OnDropDestroyed;
            drop.Deactivate();
            drop.Destroy();
            drop.Clear();

            _pool.Release(drop);
        }
    }
}
