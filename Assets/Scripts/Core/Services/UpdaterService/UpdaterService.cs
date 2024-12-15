namespace PlatformerPrototype.Core.Services
{
    internal sealed class UpdaterService : IUpdaterService
    {
        private readonly System.Collections.Generic.List<IUpdatable> _updatables = new(32);
        private readonly System.Collections.Generic.List<IFixedUpdatable> _fixedUpdatables = new(32);
        private readonly System.Collections.Generic.List<ILateUpdatable> _lateUpdatables = new(32);
        private readonly System.Collections.Generic.List<IPausable> _pausables = new(32);

        public void Tick(float deltaTime)
        {
            for (int i = 0; i < _updatables.Count; i++)
                _updatables[i].Tick(deltaTime);
        }

        public void FixedTick(float deltaTime)
        {
            for (int i = 0; i < _fixedUpdatables.Count; i++)
                _fixedUpdatables[i].FixedTick(deltaTime);
        }

        public void LateTick(float deltaTime)
        {
            for (int i = 0; i < _lateUpdatables.Count; i++)
                _lateUpdatables[i].LateTick(deltaTime);
        }

        public void SetPause(bool isPaused)
        {
            for (int i = 0; i < _pausables.Count; i++)
                _pausables[i].SetPause(isPaused);
        }

        public void AddUpdatable(IUpdatable lateUpdatable)
        {
            if (lateUpdatable != null && _updatables.Contains(lateUpdatable) == false)
                _updatables.Add(lateUpdatable);
        }

        public void RemoveUpdatable(IUpdatable updatable)
        {
            if (updatable != null && _updatables.Contains(updatable))
                _updatables.Remove(updatable);
        }

        public void AddFixedUpdatable(IFixedUpdatable fixedUpdatable)
        {
            if (fixedUpdatable != null && _fixedUpdatables.Contains(fixedUpdatable) == false)
                _fixedUpdatables.Add(fixedUpdatable);
        }

        public void RemoveFixedUpdatable(IFixedUpdatable fixedUpdatable)
        {
            if (fixedUpdatable != null && _fixedUpdatables.Contains(fixedUpdatable))
                _fixedUpdatables.Remove(fixedUpdatable);
        }

        public void AddLateUpdatable(ILateUpdatable lateUpdatable)
        {
            if (lateUpdatable != null && _lateUpdatables.Contains(lateUpdatable) == false)
                _lateUpdatables.Add(lateUpdatable);
        }

        public void RemoveLateUpdatable(ILateUpdatable lateUpdatable)
        {
            if (lateUpdatable != null && _lateUpdatables.Contains(lateUpdatable))
                _lateUpdatables.Remove(lateUpdatable);
        }

        public void AddPausable(IPausable pausable)
        {
            if (pausable != null && _pausables.Contains(pausable) == false)
                _pausables.Add(pausable);
        }

        public void RemovePausable(IPausable pausable)
        {
            if (pausable != null && _pausables.Contains(pausable))
                _pausables.Remove(pausable);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy() { }
    }
}
