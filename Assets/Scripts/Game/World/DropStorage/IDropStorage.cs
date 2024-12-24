namespace PlatformerPrototype.Game.DropStorages
{
    internal interface IDropStorage : Core.IDestroyable, World.IRestartable
    {
        public void Drop(Configs.IDropConfig dropConfig, in UnityEngine.Vector3 position);
    }
}
