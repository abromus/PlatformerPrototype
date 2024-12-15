namespace PlatformerPrototype.Game.Factories
{
    internal interface IProjectileFactory : Core.Factories.IFactory
    {
        public void Init(Core.Services.IUpdaterService updaterSevice);

        public World.Projectiles.IProjectile Create<T>(T projectilePrefab, UnityEngine.Transform container) where T : World.Projectiles.BaseProjectile;
    }
}
