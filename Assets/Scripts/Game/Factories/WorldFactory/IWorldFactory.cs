namespace PlatformerPrototype.Game.Factories
{
    internal interface IWorldFactory : Core.Factories.IFactory
    {
        public World.IWorld Create();
    }
}
