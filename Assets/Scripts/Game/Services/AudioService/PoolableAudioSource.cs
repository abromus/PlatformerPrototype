namespace PlatformerPrototype.Game.Services
{
    internal sealed class PoolableAudioSource : UnityEngine.MonoBehaviour, IPoolableAudioSource
    {
        [UnityEngine.SerializeField] private UnityEngine.AudioSource _audioSource;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Clear()
        {
            _audioSource.Stop();
            _audioSource.clip = null;
        }

        public void SetClip(UnityEngine.AudioClip clip)
        {
            if (clip == null)
                return;

            _audioSource.clip = clip;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Play()
        {
            _audioSource.Play();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Stop()
        {
            _audioSource.Stop();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            Clear();
        }
    }
}
