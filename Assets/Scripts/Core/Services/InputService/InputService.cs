namespace PlatformerPrototype.Core.Services
{
    internal sealed class InputService : IInputService
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool GetKey(UnityEngine.KeyCode key)
        {
            return UnityEngine.Input.GetKey(key);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool GetKeyDown(UnityEngine.KeyCode key)
        {
            return UnityEngine.Input.GetKeyDown(key);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public float GetHorizontalAxisRaw()
        {
            return UnityEngine.Input.GetAxisRaw(Keys.HorizontalAxis);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public float GetVerticalAxisRaw()
        {
            return UnityEngine.Input.GetAxisRaw(Keys.VerticalAxis);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy() { }

        private sealed class Keys
        {
            internal const string HorizontalAxis = "Horizontal";
            internal const string VerticalAxis = "Vertical";
        }
    }
}
