namespace PlatformerPrototype.Game.DropStorages
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct DropStorageArgs
    {
        private readonly Services.IAudioService _audioService;
        private readonly Factories.IDropFactory _factory;
        private readonly UnityEngine.Transform _container;

        internal readonly Services.IAudioService AudioService => _audioService;

        internal readonly Factories.IDropFactory Factory => _factory;

        internal readonly UnityEngine.Transform Container => _container;

        internal DropStorageArgs(
            Services.IAudioService audioService,
            Factories.IDropFactory factory,
            UnityEngine.Transform container)
        {
            _audioService = audioService;
            _factory = factory;
            _container = container;
        }
    }
}
