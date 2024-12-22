namespace PlatformerPrototype.Core.Services
{
    public interface ICameraService : IService
    {
        public UnityEngine.Transform CameraTransform { get; }

        public void Init(UnityEngine.Transform container);

        public void AttachTo(UnityEngine.Transform parent);

        public void Detach();

        public UnityEngine.Rect GetScreenRect();
    }
}
