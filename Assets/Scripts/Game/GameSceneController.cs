namespace PlatformerPrototype.Game
{
    internal sealed class GameSceneController : Core.BaseSceneController
    {
        [UnityEngine.SerializeField] private Configs.ConfigStorage _configStorage;
        [UnityEngine.SerializeField] private UnityEngine.Transform _uiServiceContainer;

        private IGame _game;

        public override void Init(Core.Data.ICoreData coreData)
        {
            _configStorage.Init();

            _game = new Game(coreData, _configStorage, _uiServiceContainer);
        }

        public override void Run()
        {
            Subscribe();

            _game.Run();
        }

        public override void Destroy()
        {
            Unsubscribe();

            _game.Destroy();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _game.Exited += OnExited;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Unsubscribe()
        {
            _game.Exited -= OnExited;
        }

        private void OnExited()
        {
            Destroy();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}
