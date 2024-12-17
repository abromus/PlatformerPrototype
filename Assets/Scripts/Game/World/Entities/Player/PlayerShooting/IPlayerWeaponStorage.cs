namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IPlayerWeaponStorage
    {
        public int CurrentAmmo { get; }

        public void Tick(float deltaTime);

        public void SetPause(bool isPaused);

        public void Restart();

        public bool TryShoot(ShootingMode shootingMode, out int index);

        public void AddAmmo(int count);
    }
}
