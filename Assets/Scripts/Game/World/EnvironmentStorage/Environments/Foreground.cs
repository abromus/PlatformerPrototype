namespace PlatformerPrototype.Game.World
{
    internal sealed class Foreground : BaseEnvironment, IForeground
    {
        public void InitPosition(in UnityEngine.Vector3 position, float offsetX, float offsetY)
        {
            var targetPosition = position;
            targetPosition.x += offsetX;
            targetPosition.y += offsetY;

            transform.position = targetPosition;
        }

        public void InitDirection(float direction)
        {
            var localScale = transform.localScale;
            localScale.x = direction;
            transform.localScale = localScale;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            Deactivate();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            Deactivate();
        }
    }
}
