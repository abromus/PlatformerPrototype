namespace PlatformerPrototype.Game.World.Health
{
    internal interface IHealthView
    {
        public void Init(IHealth health);

        public void SetActive(bool isActive);
    }
}
