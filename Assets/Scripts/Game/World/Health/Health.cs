namespace PlatformerPrototype.Game.World.Health
{
    internal sealed class Health : IHealth
    {
        private float _currentHp;
        private float _maxHp;

        public float CurrentHp => _currentHp;

        public float MaxHp => _maxHp;

        public event System.Action<IHealth> Changed;

        public void SetMaxHp(float maxHp)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(0f < maxHp);
#endif

            _maxHp = maxHp;
            _currentHp = _maxHp;

            Changed?.Invoke(this);
        }

        public void AddDamage(float damage)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(damage <= 0f);
#endif

            _currentHp += damage;

            Changed?.Invoke(this);
        }

        public void AddHp(float hp)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(0f < hp);
#endif

            _currentHp += hp;

            Changed?.Invoke(this);
        }
    }
}
