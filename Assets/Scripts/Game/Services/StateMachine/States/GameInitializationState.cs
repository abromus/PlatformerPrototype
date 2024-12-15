namespace PlatformerPrototype.Game.Services
{
    internal sealed class GameInitializationState : Core.Services.IEnterState
    {
        private readonly Data.IGameData _gameData;
        private readonly Core.Services.IStateMachine _stateMachine;

        internal GameInitializationState(Data.IGameData gameData, Core.Services.IStateMachine stateMachine)
        {
            _gameData = gameData;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            var factoryStorage = _gameData.FactoryStorage;
            var player = CreatePlayer(factoryStorage);
            var world = CreateWorld(factoryStorage);

            //_stateMachine.Enter<GameStartState, SceneInfo>(sceneInfo);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }

        private World.Entities.IPlayer CreatePlayer(Core.Factories.IFactoryStorage factoryStorage)
        {
            var factory = factoryStorage.GetFactory<Factories.IPlayerFactory>();
            var player = factory.Create();

            return player;
        }

        private World.IWorld CreateWorld(Core.Factories.IFactoryStorage factoryStorage)
        {
            var factory = factoryStorage.GetFactory<Factories.IWorldFactory>();
            var world = factory.Create();

            return world;
        }
    }
}
