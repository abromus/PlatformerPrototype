namespace PlatformerPrototype.Game.Services
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct GameOverScreenArgs : IScreenArgs
    {
        private readonly IAudioService _audioService;

        internal readonly IAudioService AudioService => _audioService;

        internal GameOverScreenArgs(IAudioService audioService)
        {
            _audioService = audioService;
        }
    }
}
