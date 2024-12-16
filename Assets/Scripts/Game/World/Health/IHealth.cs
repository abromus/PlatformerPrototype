namespace PlatformerPrototype.Game.World.Health
{
    internal interface IHealth
    {
        public float CurrentHp { get; }

        public float MaxHp { get; }

        public event System.Action<IHealth> Changed;

        public void SetMaxHp(float maxHp);

        public void AddHp(float hp);

        public void AddDamage(float damage);
    }
}
