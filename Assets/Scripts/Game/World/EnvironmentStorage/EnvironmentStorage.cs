namespace PlatformerPrototype.Game.World
{
    internal sealed class EnvironmentStorage : IEnvironmentStorage
    {
        private float _leftBackgroundPosition;
        private float _rightBackgroundPosition;
        private float _lastPlayerPosition;

        private readonly Factories.IEnvironmentFactory _factory;
        private readonly UnityEngine.Transform _playerTansform;
        private readonly UnityEngine.Transform _backgroundsContainer;
        private readonly Background _backgroundPrefab;
        private readonly float _backgroundOffsetSpeed;
        private readonly UnityEngine.Transform _chunksContainer;
        private readonly Chunk _chunkPrefab;

        private readonly System.Collections.Generic.List<Background> _backgrounds = new(32);
        private readonly System.Collections.Generic.List<Chunk> _chunks = new(32);

        public EnvironmentStorage(in EnvironmentStorageArgs args)
        {
            _factory = args.Factory;
            _playerTansform = args.PlayerTransform;
            _backgroundsContainer = args.BackgroundsContainer;
            _backgroundPrefab = args.BackgroundPrefab;
            _backgroundOffsetSpeed = args.BackgroundOffsetSpeed;
            _chunksContainer = args.ChunksContainer;
            _chunkPrefab = args.ChunkPrefab;

            var playerPosition = _playerTansform.position.x;
            _leftBackgroundPosition = playerPosition;
            _rightBackgroundPosition = playerPosition;
            _lastPlayerPosition = playerPosition;

            InitBackground(playerPosition);
            InitChunk(playerPosition);
        }

        public void LateTick(float deltaTime)
        {
            var playerPosition = _playerTansform.position.x;

            CheckPlayerPosition(playerPosition);
            TryMoveBackgrounds(playerPosition);
        }

        public void Restart()
        {
            for (int i = 0; i < _chunks.Count; i++)
                _chunks[i].Restart();
        }

        private void InitBackground(float positionX)
        {
            var background = _factory.CreateBackground(_backgroundPrefab, _backgroundsContainer);
            var position = background.transform.position;
            position.x = positionX;
            background.transform.position = position;

            _backgrounds.Add(background);
        }

        private void InitChunk(float positionX)
        {
            var chunk = _factory.CreateChunk(_chunkPrefab, _chunksContainer);
            var position = chunk.transform.position;
            position.x = positionX;
            chunk.transform.position = position;
            chunk.Restart();

            _chunks.Add(chunk);
        }

        private void CheckPlayerPosition(float playerPosition)
        {
            var offset = _backgroundPrefab.Size.x;

            if (playerPosition - offset < _leftBackgroundPosition)
            {
                _leftBackgroundPosition -= offset;

                InitBackground(_leftBackgroundPosition);
                InitChunk(_leftBackgroundPosition);
            }
            else if (_rightBackgroundPosition < playerPosition + offset)
            {
                _rightBackgroundPosition += offset;

                InitBackground(_rightBackgroundPosition);
                InitChunk(_rightBackgroundPosition);
            }
        }

        private void TryMoveBackgrounds(float playerPosition)
        {
            if (UnityEngine.Mathf.Approximately(_lastPlayerPosition, playerPosition))
                return;

            var offset = playerPosition - _lastPlayerPosition;
            var position = _backgroundsContainer.position;
            position.x += offset * _backgroundOffsetSpeed;
            _backgroundsContainer.position = position;

            _lastPlayerPosition = playerPosition;
        }
    }
}
