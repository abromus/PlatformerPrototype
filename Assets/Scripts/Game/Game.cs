namespace PlatformerPrototype.Game
{
    internal sealed class Game : IGame
    {
        private readonly Configs.ConfigStorage _configStorage;
        private readonly Data.IGameData _gameData;
        private readonly Core.Services.IStateMachine _stateMachine;

        internal Game(Core.Data.ICoreData coreData, Configs.ConfigStorage configStorage)
        {
            _configStorage = configStorage;

            _gameData = new Data.GameData(coreData, _configStorage);
            _stateMachine = _gameData.ServiceStorage.GetService<Core.Services.IStateMachine>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Run()
        {
            _stateMachine.Enter<Services.GameInitializationState>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            _gameData.Destroy();
        }
    }
}
