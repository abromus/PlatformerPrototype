namespace PlatformerPrototype.Game.DropStorages
{
    internal interface IDropStorage : Core.IDestroyable, World.IRestartable
    {
        public void Drop(Configs.IDropConfig dropConfig, UnityEngine.Vector3 position);

        public void Stop();
    }
}
