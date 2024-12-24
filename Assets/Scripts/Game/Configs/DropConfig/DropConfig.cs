namespace PlatformerPrototype.Game.Configs
{
    [System.Serializable]
    [UnityEngine.CreateAssetMenu(fileName = nameof(DropConfig), menuName = ConfigKeys.GamePathKey + nameof(DropConfig))]
    internal sealed class DropConfig : UnityEngine.ScriptableObject, IDropConfig
    {
        [UnityEngine.SerializeField] private DropType _dropType;
        [UnityEngine.SerializeField] private World.Drops.BaseDrop _baseDropPrefab;
        [UnityEngine.SerializeField] private int _minCount;
        [UnityEngine.SerializeField] private int _maxCount;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _dropClip;
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _receivedClip;

        public DropType DropType => _dropType;

        public World.Drops.BaseDrop BaseDropPrefab => _baseDropPrefab;

        public int MinCount => _minCount;

        public int MaxCount => _maxCount;

        public UnityEngine.AudioClip DropClip => _dropClip;

        public UnityEngine.AudioClip ReceivedClip => _receivedClip;
    }
}
