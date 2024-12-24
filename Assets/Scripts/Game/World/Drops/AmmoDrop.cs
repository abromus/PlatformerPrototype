namespace PlatformerPrototype.Game.World.Drops
{
    internal sealed class AmmoDrop : BaseDrop
    {
        [UnityEngine.SerializeField] private TMPro.TMP_Text _countView;

        private Services.IAudioService _audioService;
        private Configs.IDropConfig _config;
        private int _count;

        public override Configs.DropType DropType => _config.DropType;

        public override int Count => _count;

        public override event System.Action<IDrop> Destroyed;

        public override void Init(Services.IAudioService audioService, Configs.IDropConfig config)
        {
            _audioService = audioService;
            _config = config;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void InitPosition(UnityEngine.Vector3 position)
        {
            transform.position = position;
        }

        public override void Activate()
        {
            _count = UnityEngine.Random.Range(_config.MinCount, _config.MaxCount);
            _countView.text = $"{_count}";

            gameObject.SetActive(true);

            _audioService.PlayOneShotSound(_config.DropClip);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Deactivate()
        {
            gameObject.SetActive(false);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Apply()
        {
            _audioService.PlayOneShotSound(_config.ReceivedClip);

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
