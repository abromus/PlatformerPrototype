namespace PlatformerPrototype.Core.Services
{
    internal sealed class ServiceStorage : IServiceStorage
    {
        private readonly Data.ICoreData _coreData;
        private readonly System.Collections.Generic.Dictionary<System.Type, IService> _services;

        internal ServiceStorage(UnityEngine.MonoBehaviour coroutineRunner, Data.CoreData coreData)
        {
            _coreData = coreData;

            var inputService = InitInputService();
            var sceneLoader = InitSceneLoader(coroutineRunner);
            var stateMachine = InitStateMachine(sceneLoader);
            var updaterService = InitUpdaterService();

            _services = new(8)
            {
                [typeof(IInputService)] = inputService,
                [typeof(ISceneLoader)] = sceneLoader,
                [typeof(IStateMachine)] = stateMachine,
                [typeof(IUpdaterService)] = updaterService,
            };
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public TService GetService<TService>() where TService : class, IService
        {
            return _services[typeof(TService)] as TService;
        }

        public void Destroy()
        {
            var services = _services.Values;

            foreach (var service in services)
                service.Destroy();

            _services.Clear();
        }

        private IInputService InitInputService()
        {
            var inputService = new InputService();

            return inputService;
        }

        private ISceneLoader InitSceneLoader(UnityEngine.MonoBehaviour coroutineRunner)
        {
            var sceneLoader = new SceneLoader(coroutineRunner);

            return sceneLoader;
        }

        private IStateMachine InitStateMachine(ISceneLoader sceneLoader)
        {
            var stateMachine = new StateMachine();

            stateMachine.Add(new BootstrapState(stateMachine));
            stateMachine.Add(new GameState(stateMachine));
            stateMachine.Add(new GameLoopState(_coreData));
            stateMachine.Add(new SceneLoaderState(sceneLoader));

            return stateMachine;
        }

        private IUpdaterService InitUpdaterService()
        {
            var updaterService = new UpdaterService();

            return updaterService;
        }
    }
}
