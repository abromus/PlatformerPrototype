namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IZombieMovement
    {
        public void FixedTick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
