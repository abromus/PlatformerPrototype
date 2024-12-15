namespace PlatformerPrototype.Core.Services
{
    internal sealed class SceneLoader : ISceneLoader
    {
        private SceneInfo _info;

        private readonly UnityEngine.MonoBehaviour _coroutineRunner;

        internal SceneLoader(UnityEngine.MonoBehaviour coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(in SceneInfo info)
        {
            _info = info;
            _coroutineRunner.StartCoroutine(LoadAsync());
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy() { }

        private System.Collections.IEnumerator LoadAsync()
        {
            var sceneName = _info.SceneName;
            var success = _info.Success;

            if (IsSceneLoaded(sceneName))
            {
                success?.Invoke();

                yield break;
            }

            yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, _info.Mode);

            if (_info.IsActive)
            {
                var scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);

                UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);
            }

            success?.Invoke();
        }

        private bool IsSceneLoaded(string sceneName)
        {
            var sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCount;

            for (int i = 0; i < sceneCount; i++)
            {
                var scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);

                if (scene.name.Equals(sceneName))
                    return true;
            }

            return false;
        }
    }
}
