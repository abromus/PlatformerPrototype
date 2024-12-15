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
            var world = CreateWorld();
            var args = new GameStateArgs(world);

            _stateMachine.Enter<GameStartState, GameStateArgs>(args);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }

        private World.IWorld CreateWorld()
        {
            var factory = _gameData.FactoryStorage.GetFactory<Factories.IWorldFactory>();
            var world = factory.Create();

            return world;
        }
    }
}
