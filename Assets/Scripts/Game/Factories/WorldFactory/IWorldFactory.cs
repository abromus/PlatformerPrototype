namespace PlatformerPrototype.Game.Factories
{
    internal interface IWorldFactory : Core.Factories.IFactory
    {
        public void Init(Data.IGameData gameData);

        public World.IWorld Create(Core.Services.IStateMachine stateMachine);
    }
}
