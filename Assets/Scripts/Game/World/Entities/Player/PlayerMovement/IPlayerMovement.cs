namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IPlayerMovement
    {
        public void Tick(float deltaTime);

        public void FixedTick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
