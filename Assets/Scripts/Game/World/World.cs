namespace PlatformerPrototype.Game.World
{
    internal sealed class World : UnityEngine.MonoBehaviour, IWorld
    {
        [UnityEngine.SerializeField] private UnityEngine.Transform _projectileContainer;

        private Data.IGameData _gameData;
        private Entities.IPlayer _player;

        public void Init(Data.IGameData gameData)
        {
            _gameData = gameData;

            CreatePlayer();
        }

        public void Restart()
        {

        }

        public void Destroy()
        {
            _player?.Destroy();
            _gameData.Destroy();
        }

        private void CreatePlayer()
        {
            var factory = _gameData.FactoryStorage.GetFactory<Factories.IPlayerFactory>();
            
            _player = factory.Create();
            _player.Init(_gameData, _projectileContainer);
            _player.SetParent(transform);
        }
    }
}
