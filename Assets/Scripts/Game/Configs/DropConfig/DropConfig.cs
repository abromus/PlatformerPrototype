namespace PlatformerPrototype.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(DropConfig), menuName = ConfigKeys.GamePathKey + nameof(DropConfig))]
    internal sealed class DropConfig : UnityEngine.ScriptableObject, IDropConfig
    {
        [UnityEngine.SerializeField] private DropType _dropType;
        [UnityEngine.SerializeField] private World.Drops.BaseDrop _baseDropPrefab;
        [UnityEngine.SerializeField] private int _minCount;
        [UnityEngine.SerializeField] private int _maxCount;

        public DropType DropType => _dropType;

        public World.Drops.BaseDrop BaseDropPrefab => _baseDropPrefab;

        public int MinCount => _minCount;

        public int MaxCount => _maxCount;
    }
}
