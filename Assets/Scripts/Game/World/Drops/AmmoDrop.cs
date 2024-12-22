namespace PlatformerPrototype.Game.World.Drops
{
    internal sealed class AmmoDrop : BaseDrop
    {
        [UnityEngine.SerializeField] private TMPro.TMP_Text _countView;

        private Configs.DropType _dropType;
        private int _minCount;
        private int _maxCount;
        private int _count;

        public override Configs.DropType DropType => _dropType;

        public override int Count => _count;

        public override event System.Action<IDrop> Destroyed;

        public override void Init(Configs.DropType dropType, int minCount, int maxCount)
        {
            _dropType = dropType;
            _minCount = minCount;
            _maxCount = maxCount;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void InitPosition(UnityEngine.Vector3 position)
        {
            transform.position = position;
        }

        public override void Activate()
        {
            _count = UnityEngine.Random.Range(_minCount, _maxCount);
            _countView.text = $"{_count}";

            gameObject.SetActive(true);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Deactivate()
        {
            gameObject.SetActive(false);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Apply()
        {
            Destroyed?.Invoke(this);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Clear()
        {
            Destroy();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}
