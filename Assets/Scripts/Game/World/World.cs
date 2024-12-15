namespace PlatformerPrototype.Game.World
{
    internal sealed class World : UnityEngine.MonoBehaviour, IWorld
    {
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
            _player.Init(_gameData);
            _player.SetParent(transform);
        }
    }
}
