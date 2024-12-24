namespace PlatformerPrototype.Game.Configs
{
    [System.Serializable]
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal struct EnemyInfo
    {
        [UnityEngine.SerializeField] private int _index;
        [UnityEngine.SerializeField] private float _hp;
        [UnityEngine.SerializeField] private float _speed;
        [UnityEngine.SerializeField] private float _damage;
        [UnityEngine.SerializeField] private World.Entities.BaseEnemy _baseZombiePrefab;
        [UnityEngine.SerializeField] private DropConfig _dropConfig;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _runningClip;
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _deathClip;

        internal readonly int Index => _index;

        internal readonly float Hp => _hp;

        internal readonly float Speed => _speed;

        internal readonly float Damage => _damage;

        internal readonly World.Entities.BaseEnemy BaseEnemyPrefab => _baseZombiePrefab;

        internal readonly IDropConfig DropConfig => _dropConfig;

        internal readonly UnityEngine.AudioClip RunningClip => _runningClip;

        internal readonly UnityEngine.AudioClip DeathClip => _deathClip;
    }
}
