namespace PlatformerPrototype.Core.Services
{
    public interface IInputService : IService
    {
        public bool GetLeftMouseButton();

        public bool GetLeftMouseButtonDown();

        public float GetHorizontalAxisRaw();

        public float GetVerticalAxisRaw();
    }
}
