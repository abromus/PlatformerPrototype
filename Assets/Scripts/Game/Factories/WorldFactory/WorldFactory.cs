using PlatformerPrototype.Core;

namespace PlatformerPrototype.Game.Factories
{
    internal sealed class WorldFactory : Core.Factories.BaseUiFactory, IWorldFactory
    {
        [UnityEngine.SerializeField] private World.World _worldPrefab;

        private Data.IGameData _gameData;

        public void Init(Data.IGameData gameData)
        {
            _gameData = gameData;
        }

        public World.IWorld Create(Core.Services.IStateMachine stateMachine)
        {
            var world = Instantiate(_worldPrefab);
            world.gameObject.RemoveCloneSuffix();
            world.Init(_gameData, stateMachine);

            return world;
        }
    }
}
