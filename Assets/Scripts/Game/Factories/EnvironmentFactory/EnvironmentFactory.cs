using PlatformerPrototype.Core;

namespace PlatformerPrototype.Game.Factories
{
    internal sealed class EnvironmentFactory : Core.Factories.BaseUiFactory, IEnvironmentFactory
    {
        public TBackground CreateBackground<TBackground>(TBackground prefab, UnityEngine.Transform container) where TBackground : World.BaseEnvironment, World.IBackground
        {
            var background = Instantiate(prefab, container);
            background.gameObject.RemoveCloneSuffix();

            return background;
        }

        public TChunk CreateChunk<TChunk>(TChunk prefab, UnityEngine.Transform container) where TChunk : World.BaseEnvironment, World.IChunk
        {
            var chunk = Instantiate(prefab, container);
            chunk.gameObject.RemoveCloneSuffix();
            chunk.Init(this);

            return chunk;
        }

        public TBuilding CreateBuilding<TBuilding>(TBuilding prefab, UnityEngine.Transform container) where TBuilding : World.BaseEnvironment, World.IBuilding
        {
            var building = Instantiate(prefab, container);
            building.gameObject.RemoveCloneSuffix();

            return building;
        }

        public TForeground CreateForeground<TForeground>(TForeground prefab, UnityEngine.Transform container) where TForeground : World.BaseEnvironment, World.IForeground
        {
            var foreground = Instantiate(prefab, container);
            foreground.gameObject.RemoveCloneSuffix();

            return foreground;
        }
    }
}
