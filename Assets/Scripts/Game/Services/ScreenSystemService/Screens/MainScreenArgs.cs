namespace PlatformerPrototype.Game.Services
{
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
