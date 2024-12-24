namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IEnemy : IEntity, Core.IPoolable, IDamagable
    {
        public int Index { get; }

        public UnityEngine.Vector3 Position { get; }

        public UnityEngine.Vector2 Size { get; }

        public Configs.IDropConfig DropConfig { get; }

        public DeathReason DeathReason { get; }

        public event System.Action<IEnemy> Died;

        public void Init(in EnemyArgs args);

        public void InitHp();

        public void InitPosition(in UnityEngine.Vector3 position);
    }
}
