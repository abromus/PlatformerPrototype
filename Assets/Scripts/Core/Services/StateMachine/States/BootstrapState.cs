namespace PlatformerPrototype.Core.Services
{
    internal sealed class BootstrapState : IEnterState
    {
        private readonly IStateMachine _stateMachine;

        internal BootstrapState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            var sceneInfo = new SceneInfo(SceneNames.Core, UnityEngine.SceneManagement.LoadSceneMode.Single, true, OnSceneLoad);

            _stateMachine.Enter<SceneLoaderState, SceneInfo>(in sceneInfo);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }

        private void OnSceneLoad()
        {
            _stateMachine.Enter<GameState>();
        }
    }
}
