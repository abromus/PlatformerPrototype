namespace PlatformerPrototype.Core.Services
{
    internal sealed class ServiceStorage : IServiceStorage
    {
        private readonly Data.ICoreData _coreData;
        private readonly System.Collections.Generic.Dictionary<System.Type, IService> _services;

        internal ServiceStorage(
            UnityEngine.MonoBehaviour coroutineRunner,
            Data.CoreData coreData,
            UnityEngine.Transform uiServiceContainer)
        {
            _coreData = coreData;

            UnityEngine.Object.DontDestroyOnLoad(uiServiceContainer);

            var uiServices = _coreData.ConfigStorage.GetConfig<Configs.IUiServiceConfig>().UiServices;
            var cameraService = InitCameraService(uiServices, uiServiceContainer);
            var inputService = InitInputService();
            var sceneLoader = InitSceneLoader(coroutineRunner);
            var stateMachine = InitStateMachine(sceneLoader);
            var updaterService = InitUpdaterService();

            _services = new(8)
            {
                [typeof(ICameraService)] = cameraService,
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

        private ICameraService InitCameraService(IUiService[] uiServices, UnityEngine.Transform uiServiceContainer)
        {
            var cameraServicePrefab = GetService<ICameraService>(uiServices);
            var cameraService = InstantiateUiService(cameraServicePrefab as BaseUiService, uiServiceContainer) as ICameraService;
            cameraService.Init(uiServiceContainer);
            cameraService.AttachTo(uiServiceContainer);

            return cameraService;
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

        private TService GetService<TService>(IUiService[] uiServices) where TService : class, IService
        {
            for (int i = 0; i < uiServices.Length; i++)
            {
                var uiService = uiServices[i];

                if (uiService is TService service)
                    return service;
            }

            return null;
        }

        private BaseUiService InstantiateUiService(BaseUiService uiServicePrefab, UnityEngine.Transform uiServiceContainer)
        {
            var uiService = UnityEngine.Object.Instantiate(uiServicePrefab, uiServiceContainer);
            uiService.gameObject.RemoveCloneSuffix();

            return uiService;
        }
    }
}
