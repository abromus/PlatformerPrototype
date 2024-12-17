namespace PlatformerPrototype.Game.Factories
{
    internal interface IDropFactory : Core.Factories.IFactory
    {
        public World.Drops.IDrop Create(Configs.IDropConfig dropConfig, UnityEngine.Transform container);
    }
}
