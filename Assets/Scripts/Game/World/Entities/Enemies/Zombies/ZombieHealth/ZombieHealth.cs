namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class ZombieHealth : IZombieHealth
    {
        private readonly Health.HealthView _healthView;
        private readonly Health.IHealth _health = new Health.Health();

        public event System.Action Died;

        internal ZombieHealth(in ZombieHealthArgs args)
        {
            _healthView = args.HealthView;

            _health.SetMaxHp(args.Hp);
            _healthView.Init(_health);
        }

        public void InitHp()
        {
            _healthView.SetActive(true);
            _health.SetMaxHp(_health.MaxHp);
        }

        public void Change(float value)
        {
            if (0f < value)
                _health.AddHp(value);
            else
                _health.AddDamage(value);

            Check();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetActive(bool isActive)
        {
            _healthView.SetActive(isActive);
        }

        private void Check()
        {
            if (0f < _health.CurrentHp)
                return;

            Died?.Invoke();
        }
    }
}
