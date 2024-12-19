namespace PlatformerPrototype.Game.Services
{
    internal sealed class GameOverState : Core.Services.IEnterState<GameStateArgs>
    {
        private readonly IScreenSystemService _screenSystemService;

        internal GameOverState(IScreenSystemService screenSystemService)
        {
            _screenSystemService = screenSystemService;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Enter(GameStateArgs args)
        {
            _screenSystemService.HideScreens();

            _screenSystemService.Show(Configs.ScreenType.GameOver);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
