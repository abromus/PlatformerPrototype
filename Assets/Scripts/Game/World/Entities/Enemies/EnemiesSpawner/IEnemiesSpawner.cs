namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IEnemiesSpawner : Core.IDestroyable, IRestartable
    {
        public void Init(in EnemiesSpawnerArgs args);

        public void Tick(float deltaTime);

        public void FixedTick(float deltaTime);

        public void SetPause(bool isPaused);

        public void Stop();
    }
}
