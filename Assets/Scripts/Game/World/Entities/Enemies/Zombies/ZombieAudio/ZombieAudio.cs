namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class ZombieAudio : IEnemyAudio
    {
        private Services.IPoolableAudioSource _runningLoopSound;

        private readonly Services.IAudioService _audioService;
        private readonly UnityEngine.AudioClip _runningClip;
        private readonly UnityEngine.AudioClip _deathClip;

        internal ZombieAudio(in ZombieAudioArgs args)
        {
            _audioService = args.AudioService;
            _runningClip = args.RunningClip;
            _deathClip = args.DeathClip;
        }

        public void PlayRunningClip()
        {
            _runningLoopSound = _audioService.PlayLoopSound(_runningClip);
        }

        public void PlayDeathClip()
        {
            _audioService.PlayOneShotSound(_deathClip);
        }

        public void StopLoopClips()
        {
            if (_runningLoopSound == null)
                return;

            _audioService.StopLoopSound(_runningLoopSound);

            _runningLoopSound = null;
        }
    }
}
