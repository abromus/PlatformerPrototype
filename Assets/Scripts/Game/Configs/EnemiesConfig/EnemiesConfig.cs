namespace PlatformerPrototype.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(EnemiesConfig), menuName = ConfigKeys.GamePathKey + nameof(EnemiesConfig))]
    internal sealed class EnemiesConfig : UnityEngine.ScriptableObject, IEnemiesConfig
    {
        [UnityEngine.SerializeField] private int _minSpawnCount = 1;
        [UnityEngine.SerializeField] private int _maxSpawnCount = 10;
        [UnityEngine.SerializeField] private float _minSpawnDelay = 1f;
        [UnityEngine.SerializeField] private float _maxSpawnDelay = 10f;
        [UnityEngine.SerializeField] private float _minSpawnOffset = 0.5f;
        [UnityEngine.SerializeField] private float _maxSpawnOffset = 4f;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private EnemyInfo[] _enemies;

        public EnemyInfo[] Enemies => _enemies;

        public int MinSpawnCount => _minSpawnCount;

        public int MaxSpawnCount => _maxSpawnCount;

        public float MinSpawnDelay => _minSpawnDelay;

        public float MaxSpawnDelay => _maxSpawnDelay;

        public float MinSpawnOffset => _minSpawnOffset;

        public float MaxSpawnOffset => _maxSpawnOffset;
    }
}
