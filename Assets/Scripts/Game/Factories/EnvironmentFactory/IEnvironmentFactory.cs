namespace PlatformerPrototype.Game.Factories
{
    internal interface IEnvironmentFactory : Core.Factories.IFactory
    {
        public TBackground CreateBackground<TBackground>(TBackground prefab, UnityEngine.Transform container) where TBackground : World.BaseEnvironment, World.IBackground;

        public TChunk CreateChunk<TChunk>(TChunk prefab, UnityEngine.Transform container) where TChunk : World.BaseEnvironment, World.IChunk;

        public TBuilding CreateBuilding<TBuilding>(TBuilding prefab, UnityEngine.Transform container) where TBuilding : World.BaseEnvironment, World.IBuilding;

        public TForeground CreateForeground<TForeground>(TForeground prefab, UnityEngine.Transform container) where TForeground : World.BaseEnvironment, World.IForeground;
    }
}
