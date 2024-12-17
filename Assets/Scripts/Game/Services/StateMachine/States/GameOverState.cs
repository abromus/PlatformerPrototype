namespace PlatformerPrototype.Game.Services
{
    internal sealed class GameOverState : Core.Services.IEnterState<GameStateArgs>
    {
        private readonly Core.Services.IStateMachine _stateMachine;

        internal GameOverState(Core.Services.IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Enter(GameStateArgs args)
        {
            _stateMachine.Enter<GameRestartState, GameStateArgs>(args);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
