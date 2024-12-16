namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class EnemiesSpawner : UnityEngine.MonoBehaviour, IEnemiesSpawner
    {
        [UnityEngine.SerializeField] private UnityEngine.Transform _enemyContainer;

        private Core.Services.IUpdaterService _updaterService;
        private Factories.IEnemyFactory _factory;
        private Configs.IEnemiesConfig _config;
        private UnityEngine.Transform _player;
        private UnityEngine.Rect _screenRect;

        private bool _canSpawn;
        private bool _isPaused;
        private float _spawnDelay;
        private float _maxSpawnDelay;

        private readonly System.Collections.Generic.Dictionary<int, Core.IObjectPool<IEnemy>> _pools = new(16);
        private readonly System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<IEnemy>> _enemies = new(8);

        public void Init(in EnemiesSpawnerArgs args)
        {
            _updaterService = args.UpdaterService;
            _factory = args.EnemyFactory;
            _config = args.EnemyConfig;
            _player = args.Player;
            _screenRect = args.CameraService.GetScreenRect();

            _canSpawn = true;

            InitPools();
            Subscribe();
        }

        private void InitPools()
        {
            var enemyInfos = _config.Enemies;

            for (int i = 0; i < enemyInfos.Length; i++)
            {
                var enemyInfo = enemyInfos[i];
                var index = enemyInfo.Index;
                var speed = enemyInfo.Speed;
                var enemyPrefab = enemyInfo.BaseEnemyPrefab;

                _pools.Add(index, new Core.ObjectPool<IEnemy>(() => CreateEnemy(index, speed, enemyPrefab, _enemyContainer)));
            }
        }

        public void Tick(float deltaTime)
        {
            TrySpawnEnemy();
            CheckSpawnDelay(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            Unsubscribe();

            foreach (var pair in _enemies)
            {
                var enemyIndex = pair.Key;
                var enemies = pair.Value;
                var pool = _pools[enemyIndex];

                for (int i = 0; i < enemies.Count; i++)
                    pool.Release(enemies[i]);

                enemies.Clear();
                pool.Destroy();
            }

            _enemies.Clear();
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private IEnemy CreateEnemy(int enemyIndex, float speed, BaseEnemy enemyPrefab, UnityEngine.Transform enemyContainer)
        {
            var enemy = _factory.Create(enemyPrefab, enemyContainer);
            enemy.Init(_updaterService, speed);
            enemy.SetIndex(enemyIndex);

            if (_enemies.ContainsKey(enemyIndex))
                _enemies[enemyIndex].Add(enemy);
            else
                _enemies.Add(enemyIndex, new(64) { enemy });

            return enemy;
        }

        private bool TrySpawnEnemy()
        {
            if (_isPaused || _canSpawn == false)
                return false;

            _canSpawn = false;
            _spawnDelay = 0f;
            _maxSpawnDelay = UnityEngine.Random.Range(_config.MinSpawnDelay, _config.MaxSpawnDelay);

            var spawnCount = UnityEngine.Random.Range(_config.MinSpawnCount, _config.MaxSpawnCount);

            for (int i = 0; i < spawnCount; i++)
                SpawnEnemy();

            return true;
        }

        private void SpawnEnemy()
        {
            var enemyInfos = _config.Enemies;
            var enemyIndex = UnityEngine.Random.Range(0, enemyInfos.Length);
            var enemyInfo = enemyInfos[enemyIndex];

            InitEnemy(enemyInfo.Index, enemyInfo);
        }

        private void InitEnemy(int enemyIndex, Configs.EnemyInfo enemyInfo)
        {
            var enemy = _pools[enemyIndex].Get();
            var direction = UnityEngine.Random.Range(Constants.Left, Constants.Right) < Constants.Zero
                ? Constants.Left
                : Constants.Right;
            var position = _player.position;
            var halfSize = Constants.Half * enemy.Size.x;
            var screenOffset = _screenRect.size.x + halfSize;
            position.x = direction == Constants.Left ? position.x - screenOffset : position.x + screenOffset;

            enemy.InitPosition(position, _player);
            enemy.Dead += OnEnemyDead;
        }

        private void CheckSpawnDelay(float deltaTime)
        {
            if (_canSpawn)
                return;

            _spawnDelay += deltaTime;

            if (_maxSpawnDelay < _spawnDelay)
                _canSpawn = true;
        }

        private void Subscribe()
        {
            _updaterService.AddUpdatable(this);
            _updaterService.AddPausable(this);
        }

        private void Unsubscribe()
        {
            if (_updaterService != null)
            {
                _updaterService.RemoveUpdatable(this);
                _updaterService.RemovePausable(this);
            }
        }

        private void OnEnemyDead(IEnemy enemy)
        {
            enemy.Dead -= OnEnemyDead;
            enemy.Clear();

            var index = enemy.Index;

            _enemies[index].Remove(enemy);
            _pools[index].Release(enemy);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEngine.Gizmos.color = UnityEngine.Color.red;
            UnityEngine.Gizmos.DrawWireCube(transform.position, _screenRect.size);
        }
#endif
    }
}
