namespace PlatformerPrototype.Game.World
{
    internal interface IWorld : Core.IDestroyable
    {
        public void Init(Data.IGameData gameData);

        public void Restart();
    }
}
