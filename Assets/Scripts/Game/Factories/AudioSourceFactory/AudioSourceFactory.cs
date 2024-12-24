using PlatformerPrototype.Core;

namespace PlatformerPrototype.Game.Factories
{
    internal sealed class AudioSourceFactory : Core.Factories.BaseUiFactory, IAudioSourceFactory
    {
        public TAudioSource Create<TAudioSource>(TAudioSource prefab, UnityEngine.Transform container) where TAudioSource : UnityEngine.MonoBehaviour, Services.IPoolableAudioSource
        {
            var audioSourse = Instantiate(prefab, container);
            audioSourse.gameObject.RemoveCloneSuffix();

            return audioSourse;
        }
    }
}
