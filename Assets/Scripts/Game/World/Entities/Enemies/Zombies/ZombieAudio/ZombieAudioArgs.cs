namespace PlatformerPrototype.Game.World.Entities
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct ZombieAudioArgs
    {
        private readonly Services.IAudioService _audioService;
        private readonly UnityEngine.AudioClip  _runningClip;
        private readonly UnityEngine.AudioClip _deathClip;

        internal readonly Services.IAudioService AudioService => _audioService;

        internal readonly UnityEngine.AudioClip RunningClip => _runningClip;

        internal readonly UnityEngine.AudioClip DeathClip => _deathClip;

        internal ZombieAudioArgs(
            Services.IAudioService audioService,
            UnityEngine.AudioClip runningClip,
            UnityEngine.AudioClip deathClip)
        {
            _audioService = audioService;
            _runningClip = runningClip;
            _deathClip = deathClip;
        }
    }
}
