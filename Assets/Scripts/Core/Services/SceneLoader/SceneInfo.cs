namespace PlatformerPrototype.Core.Services
{
    internal readonly struct SceneInfo
    {
        private readonly string _sceneName;
        private readonly UnityEngine.SceneManagement.LoadSceneMode _mode;
        private readonly bool _isActive;
        private readonly System.Action _success;

        internal string SceneName => _sceneName;

        internal UnityEngine.SceneManagement.LoadSceneMode Mode => _mode;

        internal bool IsActive => _isActive;

        internal System.Action Success => _success;

        public SceneInfo(
            string sceneName,
            UnityEngine.SceneManagement.LoadSceneMode mode,
            bool isActive,
            System.Action success)
        {
            _sceneName = sceneName;
            _mode = mode;
            _isActive = isActive;
            _success = success;
        }
    }
}
