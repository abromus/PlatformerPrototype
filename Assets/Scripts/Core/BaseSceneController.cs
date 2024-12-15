namespace PlatformerPrototype.Core
{
    public abstract class BaseSceneController : UnityEngine.MonoBehaviour, IDestroyable
    {
        public abstract void Init();

        public abstract void Run();

        public abstract void Destroy();
    }
}
