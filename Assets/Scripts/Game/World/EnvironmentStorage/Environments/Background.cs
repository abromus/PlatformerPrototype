namespace PlatformerPrototype.Game.World
{
    internal sealed class Background : BaseEnvironment, IBackground
    {
        [UnityEngine.SerializeField] private UnityEngine.Vector2 _size;

        public UnityEngine.Vector2 Size => _size;
    }
}
