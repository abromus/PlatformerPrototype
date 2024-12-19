namespace PlatformerPrototype.Game
{
    internal interface IGame : Core.IDestroyable
    {
        public event System.Action Exited;

        public void Run();
    }
}
