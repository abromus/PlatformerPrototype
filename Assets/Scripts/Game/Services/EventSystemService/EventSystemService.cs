namespace PlatformerPrototype.Game.Services
{
    internal sealed class EventSystemService : Core.Services.BaseUiService, IEventSystemService
    {
        [UnityEngine.SerializeField] private UnityEngine.EventSystems.EventSystem _eventSystem;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool IsPointerOverGameObject()
        {
            return _eventSystem.IsPointerOverGameObject();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Destroy() { }
    }
}
