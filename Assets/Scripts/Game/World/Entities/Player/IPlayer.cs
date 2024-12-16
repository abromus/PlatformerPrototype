namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IPlayer : IEntity, Core.Services.IFixedUpdatable, Core.Services.ILateUpdatable
    {
        public UnityEngine.Transform Transform { get; }

        public void Init(Data.IGameData gameData, UnityEngine.Transform projectileContainer);

        public void SetParent(UnityEngine.Transform parent);
    }
}
