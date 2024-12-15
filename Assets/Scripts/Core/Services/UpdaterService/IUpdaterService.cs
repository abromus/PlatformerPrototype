namespace PlatformerPrototype.Core.Services
{
    public interface IUpdaterService : IService
    {
        public void Tick(float deltaTime);

        public void FixedTick(float deltaTime);

        public void LateTick(float deltaTime);

        public void SetPause(bool isPaused);

        public void AddUpdatable(IUpdatable lateUpdatable);

        public void RemoveUpdatable(IUpdatable updatable);

        public void AddFixedUpdatable(IFixedUpdatable fixedUpdatable);

        public void RemoveFixedUpdatable(IFixedUpdatable fixedUpdatable);

        public void AddLateUpdatable(ILateUpdatable lateUpdatable);

        public void RemoveLateUpdatable(ILateUpdatable lateUpdatable);

        public void AddPausable(IPausable pausable);

        public void RemovePausable(IPausable pausable);
    }
}
