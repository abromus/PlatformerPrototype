namespace PlatformerPrototype.Game.World.Entities
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct EnemyArgs
    {
        private readonly Core.Services.IUpdaterService _updaterService;
        private readonly float _hp;
        private readonly float _speed;
        private readonly float _damage;
        private readonly UnityEngine.Transform _player;

        internal readonly Core.Services.IUpdaterService UpdaterService => _updaterService;

        internal readonly float Hp => _hp;

        internal readonly float Speed => _speed;

        internal readonly float Damage => _damage;

        internal readonly UnityEngine.Transform Player => _player;

        internal EnemyArgs(
            Core.Services.IUpdaterService updaterService,
            float hp,
            float speed,
            float damage,
            UnityEngine.Transform player)
        {
            _updaterService = updaterService;
            _hp = hp;
            _speed = speed;
            _damage = damage;
            _player = player;
        }
    }
}
