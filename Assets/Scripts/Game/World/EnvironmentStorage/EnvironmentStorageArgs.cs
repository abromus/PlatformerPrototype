namespace PlatformerPrototype.Game.World
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct EnvironmentStorageArgs
    {
        private readonly Factories.IEnvironmentFactory _factory;
        private readonly UnityEngine.Transform _playerTransform;
        private readonly UnityEngine.Transform _backgroundsContainer;
        private readonly Background _backgroundPrefab;
        private readonly float _backgroundOffsetSpeed;
        private readonly UnityEngine.Transform _chunksContainer;
        private readonly Chunk _chunkPrefab;

        internal readonly Factories.IEnvironmentFactory Factory => _factory;

        internal readonly UnityEngine.Transform PlayerTransform => _playerTransform;

        internal readonly UnityEngine.Transform BackgroundsContainer => _backgroundsContainer;

        internal readonly Background BackgroundPrefab => _backgroundPrefab;

        internal readonly float BackgroundOffsetSpeed => _backgroundOffsetSpeed;

        internal readonly UnityEngine.Transform ChunksContainer => _chunksContainer;

        internal readonly Chunk ChunkPrefab => _chunkPrefab;

        internal EnvironmentStorageArgs(
            Factories.IEnvironmentFactory factory,
            UnityEngine.Transform playerTransform,
            UnityEngine.Transform backgroundsContainer,
            Background backgroundPrefab,
            float backgroundOffsetSpeed,
            UnityEngine.Transform chunksContainer,
            Chunk chunkPrefab)
        {
            _factory = factory;
            _playerTransform = playerTransform;
            _backgroundsContainer = backgroundsContainer;
            _backgroundPrefab = backgroundPrefab;
            _backgroundOffsetSpeed = backgroundOffsetSpeed;
            _chunksContainer = chunksContainer;
            _chunkPrefab = chunkPrefab;
        }
    }
}
