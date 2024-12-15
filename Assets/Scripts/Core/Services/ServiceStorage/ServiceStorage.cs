namespace PlatformerPrototype.Core.Services
{
    internal sealed class ServiceStorage : IServiceStorage
    {
        private readonly Data.ICoreData _coreData;
        private readonly System.Collections.Generic.Dictionary<System.Type, IService> _services;

        internal ServiceStorage(UnityEngine.MonoBehaviour coroutineRunner, Data.CoreData coreData)
        {
            _coreData = coreData;

            var sceneLoader = InitSceneLoader(coroutineRunner);
            var stateMachine = InitStateMachine(sceneLoader);

            _services = new(8)
            {
                [typeof(ISceneLoader)] = sceneLoader,
                [typeof(IStateMachine)] = stateMachine,
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
    }
}
