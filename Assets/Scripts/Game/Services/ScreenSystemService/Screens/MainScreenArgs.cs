namespace PlatformerPrototype.Game.Services
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct MainScreenArgs : IScreenArgs
    {
        private readonly World.Entities.IPlayer _player;

        internal readonly World.Entities.IPlayer Player => _player;

        internal MainScreenArgs(World.Entities.IPlayer player)
        {
            _player = player;
        }
    }
}
