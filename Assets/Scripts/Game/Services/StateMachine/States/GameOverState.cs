namespace PlatformerPrototype.Game.Services
{
    internal sealed class GameOverState : Core.Services.IEnterState
    {
        private readonly IScreenSystemService _screenSystemService;
        private readonly IScreenArgs _gameOverScreenArgs;

        internal GameOverState(IAudioService audioService, IScreenSystemService screenSystemService)
        {
            _screenSystemService = screenSystemService;
            _gameOverScreenArgs = new GameOverScreenArgs(audioService);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Enter()
        {
            _screenSystemService.HideScreens();

            _screenSystemService.Show(Configs.ScreenType.GameOver, in _gameOverScreenArgs);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
