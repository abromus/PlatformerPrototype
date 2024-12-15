namespace PlatformerPrototype.Game.Services
{
    internal sealed class GameOverState : Core.Services.IEnterState
    {
        private readonly Core.Services.IStateMachine _stateMachine;

        internal GameOverState(Core.Services.IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            //_sceneLoader.Load(in info);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
