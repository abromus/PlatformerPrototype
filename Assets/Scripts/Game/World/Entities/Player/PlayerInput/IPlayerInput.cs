namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IPlayerInput
    {
        public float MoveXDirection { get; }

        public bool IsShooting { get; }

        public ShootingMode ShootingMode { get; }

        public void Tick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
