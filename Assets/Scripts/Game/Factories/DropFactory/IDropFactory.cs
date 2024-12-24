namespace PlatformerPrototype.Game.Factories
{
    internal interface IDropFactory : Core.Factories.IFactory
    {
        public World.Drops.IDrop Create(Services.IAudioService audioService, Configs.IDropConfig dropConfig, UnityEngine.Transform container);
    }
}
