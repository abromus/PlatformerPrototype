namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IZombieHealth
    {
        public event System.Action Died;

        public void InitHp();

        public void Change(float value);

        public void SetActive(bool isActive);
    }
}
