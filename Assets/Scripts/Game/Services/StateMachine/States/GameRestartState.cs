namespace PlatformerPrototype.Game.Services
{
    internal sealed class GameRestartState : Core.Services.IEnterState
    {
        private readonly Core.Services.IStateMachine _stateMachine;

        internal GameRestartState(Core.Services.IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            //var sceneInfo = new SceneInfo(SceneNames.Game, UnityEngine.SceneManagement.LoadSceneMode.Additive, OnSceneLoad);

            //_stateMachine.Enter<SceneLoaderState, SceneInfo>(sceneInfo);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
