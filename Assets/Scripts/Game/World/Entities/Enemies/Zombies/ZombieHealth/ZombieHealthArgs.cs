namespace PlatformerPrototype.Game.World.Entities
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct ZombieHealthArgs
    {
        private readonly Health.HealthView _healthView;
        private readonly float _hp;

        internal readonly Health.HealthView HealthView => _healthView;

        internal readonly float Hp => _hp;

        internal ZombieHealthArgs(
            Health.HealthView healthView,
            float hp)
        {
            _healthView = healthView;
            _hp = hp;
        }
    }
}
