namespace PlatformerPrototype.Game.World
{
    internal interface IChunk
    {
        public void Init(Factories.IEnvironmentFactory factory);

        public void Restart();
    }
}
