namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IPlayerShooting : Core.IDestroyable, IRestartable
    {
        public int CurrentAmmo { get; }

        public event System.Action AmmoChanged;

        public event System.Action AmmoOut;

        public void Tick(float deltaTime);

        public void FixedTick(float deltaTime);

        public void LateTick(float deltaTime);

        public void SetPause(bool isPaused);

        public void AddAmmo(int count);

        public void StopAnimation();
    }
}
