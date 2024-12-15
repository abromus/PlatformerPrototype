namespace PlatformerPrototype.Game.Data
{
    internal interface IGameData : Core.IDestroyable
    {
        public Core.Data.ICoreData CoreData { get; }

        public Core.Configs.IConfigStorage ConfigStorage { get; }

        public Core.Services.IServiceStorage ServiceStorage { get; }

        public Core.Factories.IFactoryStorage FactoryStorage { get; }
    }
}
