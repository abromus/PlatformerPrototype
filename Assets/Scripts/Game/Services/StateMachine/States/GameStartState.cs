namespace PlatformerPrototype.Game.Services
{
    internal sealed class GameStartState : Core.Services.IEnterState
    {
        private readonly Core.Services.IStateMachine _stateMachine;

        internal GameStartState(Core.Services.IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit()
        {
        }
    }
}
