namespace PlatformerPrototype.Core.Services
{
    public interface IInputService : IService
    {
        public bool GetKey(UnityEngine.KeyCode key);

        public bool GetKeyDown(UnityEngine.KeyCode key);

        public float GetHorizontalAxisRaw();

        public float GetVerticalAxisRaw();
    }
}
