namespace PlatformerPrototype.Game.World.Entities
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct EnemiesSpawnerArgs
    {
        private readonly Core.Services.ICameraService _cameraService;
        private readonly Services.IAudioService _audioService;
        private readonly Factories.IEnemyFactory _enemyFactory;
        private readonly Configs.IEnemiesConfig _enemyConfig;
        private readonly UnityEngine.Transform _player;
        private readonly UnityEngine.Transform _enemyContainer;

        internal readonly Core.Services.ICameraService CameraService => _cameraService;

        internal readonly Services.IAudioService AudioService => _audioService;

        internal readonly Factories.IEnemyFactory EnemyFactory => _enemyFactory;

        internal readonly Configs.IEnemiesConfig EnemyConfig => _enemyConfig;

        internal readonly UnityEngine.Transform Player => _player;

        internal readonly UnityEngine.Transform EnemyContainer => _enemyContainer;

        internal EnemiesSpawnerArgs(
            Core.Services.ICameraService cameraService,
            Services.IAudioService audioService,
            Factories.IEnemyFactory enemyFactory,
            Configs.IEnemiesConfig enemyConfig,
            UnityEngine.Transform player,
            UnityEngine.Transform enemyContainer)
        {
            _cameraService = cameraService;
            _audioService = audioService;
            _enemyFactory = enemyFactory;
            _enemyConfig = enemyConfig;
            _player = player;
            _enemyContainer = enemyContainer;
        }
    }
}
