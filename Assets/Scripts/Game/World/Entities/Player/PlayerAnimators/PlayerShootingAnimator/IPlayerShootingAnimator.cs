namespace PlatformerPrototype.Game.World.Entities.Animators
{
    internal interface IPlayerShootingAnimator
    {
        public void LateTick(float deltaTime);

        public void SetPause(bool isPaused);

        public void Animate();

        public void Stop();
    }
}
