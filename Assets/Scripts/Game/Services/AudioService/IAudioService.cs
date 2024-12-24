namespace PlatformerPrototype.Game.Services
{
    internal interface IAudioService : Core.Services.IUiService, Core.Services.IPausable
    {
        public void Init(Core.Services.IUpdaterService updaterService, Factories.IAudioSourceFactory factory);

        public void PlayBackgroundMusic(UnityEngine.AudioClip clip);

        public void PlayOneShotSound(UnityEngine.AudioClip clip);

        public IPoolableAudioSource PlayLoopSound(UnityEngine.AudioClip clip);

        public void StopBackgroundMusic();

        public void StopLoopSound(IPoolableAudioSource loopSound);
    }
}
