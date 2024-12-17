namespace PlatformerPrototype.Game.Configs
{
    internal interface IEnemiesConfig : Core.Configs.IConfig
    {
        public EnemyInfo[] Enemies { get; }

        public int MinSpawnCount { get; }

        public int MaxSpawnCount { get; }

        public float MinSpawnDelay { get; }

        public float MaxSpawnDelay { get; }

        public float MinSpawnOffset { get; }

        public float MaxSpawnOffset { get; }
    }
}
