namespace PlatformerPrototype.Game.World.Entities
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct ZombieHealthArgs
    {
        private readonly Health.IHealthView _healthView;
        private readonly float _hp;

        internal readonly Health.IHealthView HealthView => _healthView;

        internal readonly float Hp => _hp;

        internal ZombieHealthArgs(
            Health.IHealthView healthView,
            float hp)
        {
            _healthView = healthView;
            _hp = hp;
        }
    }
}
