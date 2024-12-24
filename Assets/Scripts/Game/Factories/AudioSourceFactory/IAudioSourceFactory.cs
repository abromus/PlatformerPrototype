namespace PlatformerPrototype.Game.Factories
{
    internal interface IAudioSourceFactory : Core.Factories.IFactory
    {
        public TAudioSource Create<TAudioSource>(TAudioSource prefab, UnityEngine.Transform container) where TAudioSource : UnityEngine.MonoBehaviour, Services.IPoolableAudioSource;
    }
}
