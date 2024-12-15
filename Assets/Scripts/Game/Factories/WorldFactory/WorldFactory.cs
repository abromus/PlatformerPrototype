using PlatformerPrototype.Core;

namespace PlatformerPrototype.Game.Factories
{
    internal sealed class WorldFactory : Core.Factories.BaseUiFactory, IWorldFactory
    {
        [UnityEngine.SerializeField] private World.World _worldPrefab;

        public World.IWorld Create()
        {
            var world = Instantiate(_worldPrefab);
            world.gameObject.RemoveCloneSuffix();

            return world;
        }
    }
}
