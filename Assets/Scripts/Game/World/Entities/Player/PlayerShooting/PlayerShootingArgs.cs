namespace PlatformerPrototype.Game.World.Entities
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct PlayerShootingArgs
    {
        private readonly IPlayerInput _playerInput;
        private readonly UnityEngine.Transform _transform;
        private readonly Factories.IProjectileFactory _projectileFactory;
        private readonly Projectiles.BaseProjectile _projectilePrefab;
        private readonly UnityEngine.Transform _projectileContainer;
        private readonly Configs.IPlayerConfig _playerConfig;

        internal readonly IPlayerInput PlayerInput => _playerInput;

        internal readonly UnityEngine.Transform Transform => _transform;

        internal readonly Factories.IProjectileFactory ProjectileFactory => _projectileFactory;

        internal readonly Projectiles.BaseProjectile ProjectilePrefab => _projectilePrefab;

        internal UnityEngine.Transform ProjectileContainer => _projectileContainer;

        internal readonly Configs.IPlayerConfig PlayerConfig => _playerConfig;

        internal PlayerShootingArgs(
            IPlayerInput playerInput,
            UnityEngine.Transform transform,
            Factories.IProjectileFactory projectileFactory,
            Projectiles.BaseProjectile projectilePrefab,
            UnityEngine.Transform projectileContainer,
            Configs.IPlayerConfig playerConfig)
        {
            _playerInput = playerInput;
            _transform = transform;
            _projectileFactory = projectileFactory;
            _projectilePrefab = projectilePrefab;
            _projectileContainer = projectileContainer;
            _playerConfig = playerConfig;
        }
    }
}
