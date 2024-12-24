namespace PlatformerPrototype.Core.Services
{
    internal sealed class SceneLoaderState : IEnterState<SceneInfo>
    {
        private readonly ISceneLoader _sceneLoader;

        internal SceneLoaderState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter(in SceneInfo info)
        {
            _sceneLoader.Load(in info);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
