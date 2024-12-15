namespace PlatformerPrototype.Core.Factories
{
    internal sealed class GameSceneControllerFactory : BaseUiFactory, IGameSceneControllerFactory
    {
        [UnityEngine.SerializeField] private BaseSceneController _gameSceneControllerPrefab;

        public BaseSceneController Create()
        {
            var gameSceneController = Instantiate(_gameSceneControllerPrefab);
            gameSceneController.gameObject.RemoveCloneSuffix();

            return gameSceneController;
        }
    }
}
