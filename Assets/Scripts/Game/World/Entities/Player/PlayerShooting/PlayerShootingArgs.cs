namespace PlatformerPrototype.Game.World.Entities
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct PlayerShootingArgs
    {
        private readonly IPlayerInput _playerInput;
        private readonly UnityEngine.Transform _transform;
        private readonly UnityEngine.Transform _weaponTransform;
        private readonly Factories.IProjectileFactory _projectileFactory;
        private readonly UnityEngine.Transform _projectileContainer;
        private readonly Configs.IPlayerConfig _playerConfig;
        private readonly UnityEngine.Animator _animatorView;

        internal readonly IPlayerInput PlayerInput => _playerInput;

        internal readonly UnityEngine.Transform Transform => _transform;

        internal readonly UnityEngine.Transform WeaponTransform => _weaponTransform;

        internal readonly Factories.IProjectileFactory ProjectileFactory => _projectileFactory;

        internal UnityEngine.Transform ProjectileContainer => _projectileContainer;

        internal readonly Configs.IPlayerConfig PlayerConfig => _playerConfig;

        internal readonly UnityEngine.Animator AnimatorView => _animatorView;

        internal PlayerShootingArgs(
            IPlayerInput playerInput,
            UnityEngine.Transform transform,
            UnityEngine.Transform weaponTransform,
            Factories.IProjectileFactory projectileFactory,
            UnityEngine.Transform projectileContainer,
            Configs.IPlayerConfig playerConfig,
            UnityEngine.Animator animatorView)
        {
            _playerInput = playerInput;
            _transform = transform;
            _weaponTransform = weaponTransform;
            _projectileFactory = projectileFactory;
            _projectileContainer = projectileContainer;
            _playerConfig = playerConfig;
            _animatorView = animatorView;
        }
    }
}
