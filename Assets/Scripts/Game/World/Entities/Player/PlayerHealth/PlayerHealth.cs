namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerHealth : IPlayerHealth
    {
        private readonly Health.IHealth _health = new Health.Health();

        public event System.Action Dead;

        internal PlayerHealth(float hp)
        {
            _health.SetMaxHp(hp);
        }

        public void InitHp()
        {
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

        private void Check()
        {
            if (0f < _health.CurrentHp)
                return;

            Dead?.Invoke();
        }
    }
}
