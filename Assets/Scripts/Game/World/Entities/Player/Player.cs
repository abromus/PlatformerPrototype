namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class Player : UnityEngine.MonoBehaviour, IPlayer
    {
        [UnityEngine.SerializeField] private UnityEngine.Animator _animator;

        private Data.IGameData _gameData;
        private Core.Services.IUpdaterService _updaterService;
        private IPlayerInput _input;
        private IPlayerMovement _movement;
        private IPlayerShooting _shooting;

        public void Init(Data.IGameData gameData)
        {
            _gameData = gameData;

            InitModules();
            Subscribe();
        }

        public void Tick(float deltaTime)
        {
            _input.Tick(deltaTime);
            _movement.Tick(deltaTime);
        }

        public void FixedTick(float deltaTime)
        {
            _movement.FixedTick(deltaTime);
            _shooting.FixedTick(deltaTime);
        }

        public void LateTick(float deltaTime)
        {

        }

        public void SetPause(bool isPaused)
        {
            _input.SetPause(isPaused);
            _movement.SetPause(isPaused);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetParent(UnityEngine.Transform parent)
        {
            transform.SetParent(parent);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy()
        {
            Unsubscribe();
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void InitModules()
        {
            var serviceStorage = _gameData.CoreData.ServiceStorage;

            _updaterService = serviceStorage.GetService<Core.Services.IUpdaterService>();

            var inputService = serviceStorage.GetService<Core.Services.IInputService>();
            var playerConfig = _gameData.ConfigStorage.GetConfig<Configs.IPlayerConfig>();
            
            _input = new PlayerInput(inputService);
            _movement = new PlayerMovement(_input, transform, playerConfig);
            _shooting = new PlayerShooting(_input, transform, playerConfig);
            //var animator = new PlayerAnimator(_animator);
        }

        private void Subscribe()
        {
            _updaterService.AddUpdatable(this);
            _updaterService.AddFixedUpdatable(this);
            _updaterService.AddLateUpdatable(this);
            _updaterService.AddPausable(this);
        }

        private void Unsubscribe()
        {
            _updaterService.RemoveUpdatable(this);
            _updaterService.RemoveFixedUpdatable(this);
            _updaterService.RemoveLateUpdatable(this);
            _updaterService.RemovePausable(this);
        }
    }
}
