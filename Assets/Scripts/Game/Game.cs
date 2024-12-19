namespace PlatformerPrototype.Game
{
    internal sealed class Game : IGame
    {
        private readonly Configs.ConfigStorage _configStorage;
        private readonly Data.IGameData _gameData;
        private readonly Core.Services.IStateMachine _stateMachine;

        public event System.Action Exited;

        internal Game(
            Core.Data.ICoreData coreData,
            Configs.ConfigStorage configStorage,
            UnityEngine.Transform uiServiceContainer)
        {
            _configStorage = configStorage;

            _gameData = new Data.GameData(coreData, _configStorage, uiServiceContainer);
            _stateMachine = _gameData.ServiceStorage.GetService<Core.Services.IStateMachine>();
        }

        public void Run()
        {
            _stateMachine.Enter<Services.GameInitializationState>();

            Subscribe();
        }

        public void Destroy()
        {
            Unsubscribe();

            _gameData.Destroy();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _gameData.Exited += OnExited;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Unsubscribe()
        {
            if (_gameData != null)
                _gameData.Exited -= OnExited;
        }

        private void OnExited()
        {
            Exited?.Invoke();
        }
    }
}
