namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IPlayer : IEntity, IRestartable
    {
        public UnityEngine.Transform Transform { get; }

        public event System.Action Dead;

        public void Init(Data.IGameData gameData, UnityEngine.Transform projectileContainer);

        public void Tick(float deltaTime);

        public void SetParent(UnityEngine.Transform parent);
    }
}
