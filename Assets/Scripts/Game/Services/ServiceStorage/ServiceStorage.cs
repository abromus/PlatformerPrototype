namespace PlatformerPrototype.Game.Services
{
    internal sealed class ServiceStorage : Core.Services.IServiceStorage
    {
        private readonly Data.IGameData _gameData;
        private readonly System.Collections.Generic.Dictionary<System.Type, Core.Services.IService> _services;

        internal ServiceStorage(Data.GameData gameData)
        {
            _gameData = gameData;

            var stateMachine = InitStateMachine();

            _services = new(8)
            {
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

        private Core.Services.IStateMachine InitStateMachine()
        {
            var stateMachine = new Core.Services.StateMachine();

            stateMachine.Add(new GameInitializationState(_gameData, stateMachine));//создать игрока/мир
            stateMachine.Add(new GameStartState(stateMachine));//создать ui
            stateMachine.Add(new GameRestartState(stateMachine));//пересоздать врагов/обновить окружение
            stateMachine.Add(new GameLoopState(stateMachine));//игровая сессия
            stateMachine.Add(new GameOverState(stateMachine));//вывести экран

            return stateMachine;
        }
    }
}
