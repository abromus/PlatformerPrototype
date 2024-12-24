namespace PlatformerPrototype.Game.World
{
    internal sealed class Chunk : BaseEnvironment, IChunk
    {
        [UnityEngine.SerializeField] private UnityEngine.Transform _buildingSpawnPoint;
        [UnityEngine.SerializeField] private Building[] _buildingPrefabs;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.Transform _foregroundOneSpawnPoint;
        [UnityEngine.SerializeField] private Foreground[] _foregroundOnePrefabs;
        [UnityEngine.SerializeField] private UnityEngine.Vector2 _minForegroundOneSpawnOffset;
        [UnityEngine.SerializeField] private UnityEngine.Vector2 _maxForegroundOneSpawnOffset;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.Transform _foregroundTwoSpawnPoint;
        [UnityEngine.SerializeField] private Foreground[] _foregroundTwoPrefabs;
        [UnityEngine.SerializeField] private UnityEngine.Vector2 _minForegroundTwoSpawnOffset;
        [UnityEngine.SerializeField] private UnityEngine.Vector2 _maxForegroundTwoSpawnOffset;

        private Factories.IEnvironmentFactory _factory;

        private readonly System.Collections.Generic.Dictionary<int, Core.IObjectPool<IBuilding>> _buildingPools = new(4);
        private readonly System.Collections.Generic.Dictionary<int, Core.IObjectPool<IForeground>> _foregroundOnePools = new(4);
        private readonly System.Collections.Generic.Dictionary<int, Core.IObjectPool<IForeground>> _foregroundTwoPools = new(4);
        private readonly System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<IBuilding>> _buildings = new(64);
        private readonly System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<IForeground>> _foregroundOnes = new(64);
        private readonly System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<IForeground>> _foregroundTwos = new(64);

        public void Init(Factories.IEnvironmentFactory factory)
        {
            _factory = factory;

            InitPools();
        }

        public void Restart()
        {
            ClearBuildings();
            ClearForegrounds(_foregroundOnes, _foregroundOnePools);
            ClearForegrounds(_foregroundTwos, _foregroundTwoPools);

            InitBuilding();
            InitForegrounds(
                _foregroundOnePools,
                _foregroundOnes,
                _foregroundOneSpawnPoint,
                _minForegroundOneSpawnOffset,
                _maxForegroundOneSpawnOffset);
            InitForegrounds(
                _foregroundTwoPools,
                _foregroundTwos,
                _foregroundTwoSpawnPoint,
                _minForegroundTwoSpawnOffset,
                _maxForegroundTwoSpawnOffset);
        }

        private void InitPools()
        {
            for (int i = 0; i < _buildingPrefabs.Length; i++)
            {
                var prefab = _buildingPrefabs[i];

                _buildingPools.Add(i, new Core.ObjectPool<IBuilding>(() => CreateBuilding(prefab)));
            }

            for (int i = 0; i < _foregroundOnePrefabs.Length; i++)
            {
                var prefab = _foregroundOnePrefabs[i];

                _foregroundOnePools.Add(i, new Core.ObjectPool<IForeground>(() => CreateForeground(prefab, _foregroundOneSpawnPoint)));
            }

            for (int i = 0; i < _foregroundTwoPrefabs.Length; i++)
            {
                var prefab = _foregroundTwoPrefabs[i];

                _foregroundTwoPools.Add(i, new Core.ObjectPool<IForeground>(() => CreateForeground(prefab, _foregroundTwoSpawnPoint)));
            }
        }

        private void InitBuilding()
        {
            var index = UnityEngine.Random.Range(0, _buildingPrefabs.Length);
            var building = _buildingPools[index].Get();
            building.Activate();

            if (_buildings.ContainsKey(index))
                _buildings[index].Add(building);
            else
                _buildings.Add(index, new(32) { building });
        }

        private void InitForegrounds(
            System.Collections.Generic.Dictionary<int, Core.IObjectPool<IForeground>> pools,
            System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<IForeground>> foregrounds,
            UnityEngine.Transform spawnPoint,
            UnityEngine.Vector2 minOffset,
            UnityEngine.Vector2 maxOffset)
        {
            var index = UnityEngine.Random.Range(0, pools.Count);
            var pool = pools[index];
            var foreground = pool.Get();
            var offsetX = UnityEngine.Random.Range(minOffset.x, maxOffset.x);
            var offsetY = UnityEngine.Random.Range(minOffset.y, maxOffset.y);
            var position = spawnPoint.position;
            var direction = Constants.Half < UnityEngine.Random.value ? Constants.Left : Constants.Right;
            var isActive = Constants.Half < UnityEngine.Random.value;
            foreground.InitPosition(in position, offsetX, offsetY);
            foreground.InitDirection(direction);

            if (isActive)
                foreground.Activate();
            else
                foreground.Deactivate();

            if (foregrounds.ContainsKey(index))
                foregrounds[index].Add(foreground);
            else
                foregrounds.Add(index, new(32) { foreground });
        }

        private IBuilding CreateBuilding(Building prefab)
        {
            var building = _factory.CreateBuilding(prefab, _buildingSpawnPoint);

            return building;
        }

        private IForeground CreateForeground(Foreground prefab, UnityEngine.Transform container)
        {
            var foreground = _factory.CreateForeground(prefab, container);

            return foreground;
        }

        private void ClearBuildings()
        {
            foreach (var pair in _buildings)
            {
                var index = pair.Key;
                var buildings = pair.Value;
                var pool = _buildingPools[index];

                for (int i = 0; i < buildings.Count; i++)
                {
                    var building = buildings[i];
                    building.Deactivate();
                    building.Clear();
                    pool.Release(building);
                }

                buildings.Clear();
            }
        }

        private void ClearForegrounds(System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<IForeground>> storage,
            System.Collections.Generic.Dictionary<int, Core.IObjectPool<IForeground>> pools)
        {
            foreach (var pair in storage)
            {
                var index = pair.Key;
                var foregrounds = pair.Value;
                var pool = pools[index];

                for (int i = 0; i < foregrounds.Count; i++)
                {
                    var foreground = foregrounds[i];
                    foreground.Deactivate();
                    foreground.Clear();
                    pool.Release(foreground);
                }

                foregrounds.Clear();
            }
        }
    }
}
