namespace PlatformerPrototype.Game.World
{
    internal interface IForeground : Core.IPoolable
    {
        public void InitPosition(in UnityEngine.Vector3 position, float offsetX, float offsetY);

        public void InitDirection(float direction);
    }
}
