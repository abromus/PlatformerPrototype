namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IPlayer : IEntity, Core.Services.IFixedUpdatable, Core.Services.ILateUpdatable
    {
        public void Init(Data.IGameData gameData, UnityEngine.Transform projectileContainer);

        public void SetParent(UnityEngine.Transform parent);
    }
}
