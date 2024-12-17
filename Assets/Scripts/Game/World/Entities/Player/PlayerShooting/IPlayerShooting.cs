namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IPlayerShooting : Core.IDestroyable, IRestartable
    {
        public void Tick(float deltaTime);

        public void FixedTick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
