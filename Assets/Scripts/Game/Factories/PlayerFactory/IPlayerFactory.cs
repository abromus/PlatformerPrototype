namespace PlatformerPrototype.Game.Factories
{
    internal interface IPlayerFactory : Core.Factories.IFactory
    {
        public World.Entities.IPlayer Create();
    }
}
