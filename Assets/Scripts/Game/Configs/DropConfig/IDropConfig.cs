namespace PlatformerPrototype.Game.Configs
{
    internal interface IDropConfig : Core.Configs.IConfig
    {
        public DropType DropType { get; }

        public World.Drops.BaseDrop BaseDropPrefab { get; }

        public int MinCount { get; }

        public int MaxCount { get; }
    }
}
