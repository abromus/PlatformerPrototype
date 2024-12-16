namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IEnemy : IEntity, Core.IPoolable
    {
        public int Index { get; }

        public UnityEngine.Vector2 Size { get; }

        public event System.Action<IEnemy> Dead;

        public void Init(Core.Services.IUpdaterService updaterSevice, float speed);

        public void InitPosition(UnityEngine.Vector3 position, UnityEngine.Transform player);

        public abstract void SetIndex(int index);
    }
}
