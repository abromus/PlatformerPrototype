namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IEntity : Core.IDestroyable
    {
        public void FixedTick(float deltaTime);

        public void LateTick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
