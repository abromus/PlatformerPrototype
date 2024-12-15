namespace PlatformerPrototype.Game.Services
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct GameStateArgs
    {
        private readonly World.IWorld _world;

        internal readonly World.IWorld World => _world;

        internal GameStateArgs(World.IWorld world)
        {
            _world = world;
        }
    }
}
