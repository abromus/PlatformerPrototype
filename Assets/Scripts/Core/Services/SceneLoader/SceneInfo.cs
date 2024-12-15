namespace PlatformerPrototype.Core.Services
{
    internal readonly struct SceneInfo
    {
        private readonly string _sceneName;
        private readonly UnityEngine.SceneManagement.LoadSceneMode _mode;
        private readonly System.Action _success;

        internal string SceneName => _sceneName;

        internal UnityEngine.SceneManagement.LoadSceneMode Mode => _mode;

        internal System.Action Success => _success;

        public SceneInfo(
            string sceneName,
            UnityEngine.SceneManagement.LoadSceneMode mode,
            System.Action success)
        {
            _sceneName = sceneName;
            _mode = mode;
            _success = success;
        }
    }
}
