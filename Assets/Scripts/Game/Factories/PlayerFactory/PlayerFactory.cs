using PlatformerPrototype.Core;

namespace PlatformerPrototype.Game.Factories
{
    internal sealed class PlayerFactory : Core.Factories.BaseUiFactory, IPlayerFactory
    {
        [UnityEngine.SerializeField] private World.Entities.Player _playerPrefab;

        public World.Entities.IPlayer Create()
        {
            var player = Instantiate(_playerPrefab);
            player.gameObject.RemoveCloneSuffix();

            return player;
        }
    }
}
