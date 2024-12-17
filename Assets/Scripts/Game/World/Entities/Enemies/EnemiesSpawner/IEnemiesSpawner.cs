namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IEnemiesSpawner : Core.IDestroyable, IRestartable
    {
        public event System.Action<Configs.IDropConfig, UnityEngine.Vector3> Dropped;

        public void Tick(float deltaTime);

        public void FixedTick(float deltaTime);

        public void SetPause(bool isPaused);

        public void Stop();
    }
}
