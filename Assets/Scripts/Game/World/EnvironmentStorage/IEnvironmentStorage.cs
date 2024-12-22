namespace PlatformerPrototype.Game.World
{
    internal interface IEnvironmentStorage : IRestartable
    {
        public void LateTick(float deltaTime);
    }
}
