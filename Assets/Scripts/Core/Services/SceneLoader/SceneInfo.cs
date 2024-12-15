namespace PlatformerPrototype.Core.Services
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct SceneInfo
    {
        private readonly string _sceneName;
        private readonly UnityEngine.SceneManagement.LoadSceneMode _mode;
        private readonly bool _isActive;
        private readonly System.Action _success;

        internal readonly string SceneName => _sceneName;

        internal readonly UnityEngine.SceneManagement.LoadSceneMode Mode => _mode;

        internal readonly bool IsActive => _isActive;

        internal readonly System.Action Success => _success;

        internal SceneInfo(
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
