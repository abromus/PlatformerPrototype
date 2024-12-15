namespace PlatformerPrototype.Core.Services
{
    internal sealed class GameState : IEnterState
    {
        private readonly IStateMachine _stateMachine;

        internal GameState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            var sceneInfo = new SceneInfo(SceneNames.Game, UnityEngine.SceneManagement.LoadSceneMode.Additive, OnSceneLoad);

            _stateMachine.Enter<SceneLoaderState, SceneInfo>(sceneInfo);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }

        private void OnSceneLoad()
        {
            _stateMachine.Enter<GameLoopState>();
        }
    }
}
