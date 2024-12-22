namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IAnimator
    {
        public void LateTick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
