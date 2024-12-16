namespace PlatformerPrototype.Game.Configs
{
    [System.Serializable]
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal struct EnemyInfo
    {
        [UnityEngine.SerializeField] private int _index;
        [UnityEngine.SerializeField] private float _hp;
        [UnityEngine.SerializeField] private float _speed;
        [UnityEngine.SerializeField] private World.Entities.BaseEnemy _baseZombiePrefab;

        internal readonly int Index => _index;

        internal readonly float Hp => _hp;

        internal readonly float Speed => _speed;

        internal readonly World.Entities.BaseEnemy BaseEnemyPrefab => _baseZombiePrefab;
    }
}
