namespace PlatformerPrototype.Core
{
    internal sealed class Bootstrapper : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private CoreSceneController _controller;

        private void Awake()
        {
            _controller.Init();
            _controller.CreateCoreData();
        }
    }
}
