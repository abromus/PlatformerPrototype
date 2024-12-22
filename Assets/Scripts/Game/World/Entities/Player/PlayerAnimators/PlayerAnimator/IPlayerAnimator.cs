namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IPlayerAnimator
    {
        public void LateTick(float deltaTime);

        public void SetPause(bool isPaused);

        public void Stop();
    }
}
