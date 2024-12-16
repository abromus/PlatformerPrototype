namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IEnemiesSpawner : Core.IDestroyable, Core.Services.IUpdatable, Core.Services.IPausable
    {
        public void Init(in EnemiesSpawnerArgs args);
    }
}
