namespace PlatformerPrototype.Game.Services
{
    internal sealed class AudioService : Core.Services.BaseUiService, IAudioService
    {
        [UnityEngine.SerializeField] private UnityEngine.AudioSource _backgroundMusic;
        [UnityEngine.SerializeField] private UnityEngine.AudioSource _oneShotSound;
        [UnityEngine.SerializeField] private PoolableAudioSource _loopSoundPrefab;
        [UnityEngine.SerializeField] private UnityEngine.Transform _loopSoundsContainer;

        private Core.Services.IUpdaterService _updaterService;
        private Factories.IAudioSourceFactory _factory;
        private Core.IObjectPool<IPoolableAudioSource> _pool;

        private readonly System.Collections.Generic.List<IPoolableAudioSource> _loopSounds = new(64);

        public void Init(Core.Services.IUpdaterService updaterService, Factories.IAudioSourceFactory factory)
        {
            _updaterService = updaterService;
            _factory = factory;

            _pool = new Core.ObjectPool<IPoolableAudioSource>(CreatePoolableAudioSource);

            Subscribe();
        }

        public void SetPause(bool isPaused)
        {
            if (isPaused)
                _backgroundMusic.Pause();
            else
                _backgroundMusic.UnPause();
        }

        public void PlayBackgroundMusic(UnityEngine.AudioClip clip)
        {
            if (clip == null)
                return;

            _backgroundMusic.Stop();
            _backgroundMusic.clip = clip;
            _backgroundMusic.loop = true;
            _backgroundMusic.Play();
        }

        public void PlayOneShotSound(UnityEngine.AudioClip clip)
        {
            if (clip == null)
                return;

            _oneShotSound.PlayOneShot(clip);
        }

        public IPoolableAudioSource PlayLoopSound(UnityEngine.AudioClip clip)
        {
            if (clip == null)
                return null;

            var loopSound = _pool.Get();
            loopSound.SetClip(clip);
            loopSound.Activate();
            loopSound.Play();

            _loopSounds.Add(loopSound);

            return loopSound;
        }

        public void StopBackgroundMusic()
        {
            _backgroundMusic.clip = null;
            _backgroundMusic.Stop();
        }

        public void StopLoopSound(IPoolableAudioSource loopSound)
        {
            loopSound.Stop();
            loopSound.Deactivate();
            loopSound.Clear();

            _loopSounds.Remove(loopSound);
            _pool.Release(loopSound);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Destroy()
        {
            Unsubscribe();
        }

        private IPoolableAudioSource CreatePoolableAudioSource()
        {
            var drop = _factory.Create(_loopSoundPrefab, _loopSoundsContainer);

            _loopSounds.Add(drop);

            return drop;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _updaterService.AddPausable(this);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Unsubscribe()
        {
            if (_updaterService != null)
                _updaterService.RemovePausable(this);
        }
    }
}
