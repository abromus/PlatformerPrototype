namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IPlayerMovement
    {
        public bool IsMoving { get; }

        public void Tick(float deltaTime);

        public void FixedTick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
