namespace PlatformerPrototype.Game.World.Entities.Animators
{
    internal interface IPlayerShootingAnimator : IAnimator
    {
        public void Animate();

        public void Stop();
    }
}
