namespace PlatformerPrototype.Game.Services
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct MainScreenArgs : IScreenArgs
    {
        private readonly IAudioService _audioService;
        private readonly World.Entities.IPlayer _player;

        internal readonly IAudioService AudioService => _audioService;

        internal readonly World.Entities.IPlayer Player => _player;

        internal MainScreenArgs(IAudioService audioService, World.Entities.IPlayer player)
        {
            _audioService = audioService;
            _player = player;
        }
    }
}
