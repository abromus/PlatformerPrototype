namespace PlatformerPrototype.Game.Services
{
    internal interface IPoolableAudioSource : Core.IPoolable
    {
        public void SetClip(UnityEngine.AudioClip clip);

        public void Play();

        public void Stop();
    }
}
