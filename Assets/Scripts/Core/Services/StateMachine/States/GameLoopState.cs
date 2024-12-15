namespace PlatformerPrototype.Core.Services
{
    internal sealed class GameLoopState : IEnterState
    {
        private BaseSceneController _gameSceneController;

        private readonly Data.ICoreData _coreData;

        internal GameLoopState(Data.ICoreData coreData)
        {
            _coreData = coreData;
        }

        public void Enter()
        {
            var gameSceneControllerFactory = _coreData.FactoryStorage.GetFactory<Factories.IGameSceneControllerFactory>();

            _gameSceneController = gameSceneControllerFactory.Create();
            _gameSceneController.Init(_coreData);
            _gameSceneController.Run();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit()
        {
            if (_gameSceneController != null && _gameSceneController.gameObject != null)
                _gameSceneController.Destroy();
        }
    }
}
