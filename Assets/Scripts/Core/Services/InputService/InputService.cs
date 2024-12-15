namespace PlatformerPrototype.Core.Services
{
    internal sealed class InputService : IInputService
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool GetLeftMouseButton()
        {
            return UnityEngine.Input.GetMouseButton(Keys.LeftButton);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool GetLeftMouseButtonDown()
        {
            return UnityEngine.Input.GetMouseButtonDown(Keys.LeftButton);
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
            internal const int LeftButton = 0;

            internal const string HorizontalAxis = "Horizontal";
            internal const string VerticalAxis = "Vertical";
        }
    }
}
