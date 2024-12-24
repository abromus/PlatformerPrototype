namespace PlatformerPrototype.Game.Services
{
    internal sealed class GameRestartState : Core.Services.IEnterState<GameStateArgs>
    {
        private readonly Core.Services.IStateMachine _stateMachine;

        internal GameRestartState(Core.Services.IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(in GameStateArgs args)
        {
            args.World.Restart();

            _stateMachine.Enter<GameLoopState>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
