namespace PlatformerPrototype.Core.Services
{
    internal sealed class UpdaterService : IUpdaterService
    {
        private readonly System.Collections.Generic.List<IUpdatable> _updatables = new(32);
        private readonly System.Collections.Generic.List<IFixedUpdatable> _fixedUpdatables = new(32);
        private readonly System.Collections.Generic.List<ILateUpdatable> _lateUpdatables = new(32);
        private readonly System.Collections.Generic.List<IPausable> _pausables = new(32);

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick(float deltaTime)
        {
            for (int i = 0; i < _updatables.Count; i++)
                _updatables[i].Tick(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void FixedTick(float deltaTime)
        {
            for (int i = 0; i < _fixedUpdatables.Count; i++)
                _fixedUpdatables[i].FixedTick(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void LateTick(float deltaTime)
        {
            for (int i = 0; i < _lateUpdatables.Count; i++)
                _lateUpdatables[i].LateTick(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            for (int i = 0; i < _pausables.Count; i++)
                _pausables[i].SetPause(isPaused);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void AddUpdatable(IUpdatable lateUpdatable)
        {
            if (lateUpdatable != null && _updatables.Contains(lateUpdatable) == false)
                _updatables.Add(lateUpdatable);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void RemoveUpdatable(IUpdatable updatable)
        {
            if (updatable != null && _updatables.Contains(updatable))
                _updatables.Remove(updatable);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void AddFixedUpdatable(IFixedUpdatable fixedUpdatable)
        {
            if (fixedUpdatable != null && _fixedUpdatables.Contains(fixedUpdatable) == false)
                _fixedUpdatables.Add(fixedUpdatable);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void RemoveFixedUpdatable(IFixedUpdatable fixedUpdatable)
        {
            if (fixedUpdatable != null && _fixedUpdatables.Contains(fixedUpdatable))
                _fixedUpdatables.Remove(fixedUpdatable);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void AddLateUpdatable(ILateUpdatable lateUpdatable)
        {
            if (lateUpdatable != null && _lateUpdatables.Contains(lateUpdatable) == false)
                _lateUpdatables.Add(lateUpdatable);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void RemoveLateUpdatable(ILateUpdatable lateUpdatable)
        {
            if (lateUpdatable != null && _lateUpdatables.Contains(lateUpdatable))
                _lateUpdatables.Remove(lateUpdatable);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void AddPausable(IPausable pausable)
        {
            if (pausable != null && _pausables.Contains(pausable) == false)
                _pausables.Add(pausable);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void RemovePausable(IPausable pausable)
        {
            if (pausable != null && _pausables.Contains(pausable))
                _pausables.Remove(pausable);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy() { }
    }
}
