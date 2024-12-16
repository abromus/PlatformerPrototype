﻿namespace PlatformerPrototype.Game.World.Entities
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct EnemiesSpawnerArgs
    {
        private readonly Core.Services.ICameraService _cameraService;
        private readonly Core.Services.IUpdaterService _updaterService;
        private readonly Factories.IEnemyFactory _enemyFactory;
        private readonly Configs.IEnemiesConfig _enemyConfig;
        private readonly UnityEngine.Transform _player;

        internal readonly Core.Services.ICameraService CameraService => _cameraService;

        internal readonly Core.Services.IUpdaterService UpdaterService => _updaterService;

        internal readonly Factories.IEnemyFactory EnemyFactory => _enemyFactory;

        internal readonly Configs.IEnemiesConfig EnemyConfig => _enemyConfig;

        internal readonly UnityEngine.Transform Player => _player;

        internal EnemiesSpawnerArgs(
            Core.Services.ICameraService cameraService,
            Core.Services.IUpdaterService updaterService,
            Factories.IEnemyFactory enemyFactory,
            Configs.IEnemiesConfig enemyConfig,
            UnityEngine.Transform player)
        {
            _cameraService = cameraService;
            _updaterService = updaterService;
            _enemyFactory = enemyFactory;
            _enemyConfig = enemyConfig;
            _player = player;
        }
    }
}
