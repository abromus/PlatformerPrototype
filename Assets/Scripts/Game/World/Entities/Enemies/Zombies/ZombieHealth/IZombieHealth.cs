namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IZombieHealth
    {
        public event System.Action Dead;

        public void InitHp();

        public void Change(float value);

        public void SetActive(bool isActive);
    }
}
