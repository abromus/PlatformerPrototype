namespace PlatformerPrototype.Core.Services
{
    public interface ICameraService : IService
    {
        public void Init(UnityEngine.Transform container);

        public void AttachTo(UnityEngine.Transform parent);

        public void Detach();

        public UnityEngine.Rect GetScreenRect();
    }
}
