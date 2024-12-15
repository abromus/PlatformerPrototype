namespace PlatformerPrototype.Game
{
    internal sealed class GameSceneController : Core.BaseSceneController
    {
        [UnityEngine.SerializeField] private Configs.ConfigStorage _configStorage;

        private IGame _game;

        public override void Init(Core.Data.ICoreData coreData)
        {
            _configStorage.Init();

            _game = new Game(coreData, _configStorage);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Run()
        {
            _game.Run();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Destroy()
        {
            _game.Destroy();
        }
    }
}
