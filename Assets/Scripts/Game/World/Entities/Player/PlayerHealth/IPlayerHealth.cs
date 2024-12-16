namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IPlayerHealth
    {
        public event System.Action Dead;

        public void InitHp();

        public void Change(float value);
    }
}
