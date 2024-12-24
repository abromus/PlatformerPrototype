namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerAudio : IPlayerAudio
    {
        private readonly Services.IAudioService _audioService;
        private readonly UnityEngine.AudioClip _deathClip;

        internal PlayerAudio(Services.IAudioService audioService, UnityEngine.AudioClip deathClip)
        {
            _audioService = audioService;
            _deathClip = deathClip;
        }

        public void PlayDeathClip()
        {
            _audioService.PlayOneShotSound(_deathClip);
        }
    }
}
