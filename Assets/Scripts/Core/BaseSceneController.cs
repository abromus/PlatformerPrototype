namespace PlatformerPrototype.Core
{
    public abstract class BaseSceneController : UnityEngine.MonoBehaviour, IDestroyable
    {
        public abstract void Init(Data.ICoreData coreData);

        public abstract void Run();

        public abstract void Destroy();
    }
}
