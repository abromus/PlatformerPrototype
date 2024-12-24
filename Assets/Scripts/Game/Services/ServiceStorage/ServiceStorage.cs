using PlatformerPrototype.Core;

namespace PlatformerPrototype.Game.Services
{
    internal sealed class ServiceStorage : Core.Services.IServiceStorage
    {
        private readonly Data.IGameData _gameData;
        private readonly System.Collections.Generic.Dictionary<System.Type, Core.Services.IService> _services;

        internal ServiceStorage(Data.GameData gameData, UnityEngine.Transform uiServiceContainer)
        {
            _gameData = gameData;

            uiServiceContainer.SetParent(null);

            var audioSourceFactory = _gameData.FactoryStorage.GetFactory<Factories.IAudioSourceFactory>();
            var updaterService = _gameData.CoreData.ServiceStorage.GetService<Core.Services.IUpdaterService>();
            var uiServices = _gameData.ConfigStorage.GetConfig<Configs.IUiServiceConfig>().UiServices;
            var audioService = InitAudioService(updaterService, uiServices, audioSourceFactory, uiServiceContainer);
            var eventSystemService = InitEventSystemService(uiServices, uiServiceContainer);
            var screenSystemService = InitScreenSystemService(uiServices, uiServiceContainer);
            var stateMachine = InitStateMachine(audioService, screenSystemService);

            _services = new(8)
            {
                [typeof(IAudioService)] = audioService,
                [typeof(IEventSystemService)] = eventSystemService,
                [typeof(IScreenSystemService)] = screenSystemService,
                [typeof(Core.Services.IStateMachine)] = stateMachine,
            };
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public TService GetService<TService>() where TService : class, Core.Services.IService
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

        private IAudioService InitAudioService(Core.Services.IUpdaterService updaterService, Core.Services.IUiService[] uiServices, Factories.IAudioSourceFactory audioSourceFactory, UnityEngine.Transform uiServiceContainer)
        {
            var audioServicePrefab = GetService<IAudioService>(uiServices);
            var audioService = InstantiateUiService(audioServicePrefab as Core.Services.BaseUiService, uiServiceContainer) as IAudioService;
            audioService.Init(updaterService, audioSourceFactory);

            return audioService;
        }

        private IEventSystemService InitEventSystemService(Core.Services.IUiService[] uiServices, UnityEngine.Transform uiServiceContainer)
        {
            var eventSystemServicePrefab = GetService<IEventSystemService>(uiServices);
            var eventSystemService = InstantiateUiService(eventSystemServicePrefab as Core.Services.BaseUiService, uiServiceContainer) as IEventSystemService;

            return eventSystemService;
        }

        private IScreenSystemService InitScreenSystemService(Core.Services.IUiService[] uiServices, UnityEngine.Transform uiServiceContainer)
        {
            var screenSystemServicePrefab = GetService<IScreenSystemService>(uiServices);
            var screenSystemService = InstantiateUiService(screenSystemServicePrefab as Core.Services.BaseUiService, uiServiceContainer) as IScreenSystemService;
            screenSystemService.Init(_gameData);
            screenSystemService.AttachTo(uiServiceContainer);

            return screenSystemService;
        }

        private Core.Services.IStateMachine InitStateMachine(IAudioService audioService, IScreenSystemService screenSystemService)
        {
            var stateMachine = new Core.Services.StateMachine();

            stateMachine.Add(new GameInitializationState(_gameData, stateMachine));
            stateMachine.Add(new GameStartState(stateMachine));
            stateMachine.Add(new GameRestartState(stateMachine));
            stateMachine.Add(new GameLoopState());
            stateMachine.Add(new GameOverState(audioService, screenSystemService));

            return stateMachine;
        }

        private TService GetService<TService>(Core.Services.IUiService[] uiServices) where TService : class, Core.Services.IService
        {
            for (int i = 0; i < uiServices.Length; i++)
            {
                var uiService = uiServices[i];

                if (uiService is TService service)
                    return service;
            }

            return null;
        }

        private Core.Services.BaseUiService InstantiateUiService(Core.Services.BaseUiService uiServicePrefab, UnityEngine.Transform uiServiceContainer)
        {
            var uiService = UnityEngine.Object.Instantiate(uiServicePrefab, uiServiceContainer);
            uiService.gameObject.RemoveCloneSuffix();

            return uiService;
        }
    }
}
