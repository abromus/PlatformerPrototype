namespace PlatformerPrototype.Game.World.Entities
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct EnemyArgs
    {
        private readonly float _hp;
        private readonly float _speed;
        private readonly float _damage;
        private readonly Configs.IDropConfig _dropConfig;
        private readonly UnityEngine.Transform _player;

        internal readonly float Hp => _hp;

        internal readonly float Speed => _speed;

        internal readonly float Damage => _damage;

        internal readonly Configs.IDropConfig DropConfig => _dropConfig;

        internal readonly UnityEngine.Transform Player => _player;

        internal EnemyArgs(
            float hp,
            float speed,
            float damage,
            Configs.IDropConfig dropConfig,
            UnityEngine.Transform player)
        {
            _hp = hp;
            _speed = speed;
            _damage = damage;
            _dropConfig = dropConfig;
            _player = player;
        }
    }
}
