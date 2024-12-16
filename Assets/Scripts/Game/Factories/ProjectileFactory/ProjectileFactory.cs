using PlatformerPrototype.Core;

namespace PlatformerPrototype.Game.Factories
{
    internal sealed class ProjectileFactory : Core.Factories.BaseUiFactory, IProjectileFactory
    {
        private Core.Services.IUpdaterService _updaterService;

        public void Init(Core.Services.IUpdaterService updaterSevice)
        {
            _updaterService = updaterSevice;
        }

        public World.Projectiles.IProjectile Create<T>(T projectilePrefab, UnityEngine.Transform container) where T : World.Projectiles.BaseProjectile
        {
            var projectile = Instantiate(projectilePrefab, container);
            projectile.gameObject.RemoveCloneSuffix();
            projectile.Init(_updaterService);

            return projectile;
        }
    }
}
