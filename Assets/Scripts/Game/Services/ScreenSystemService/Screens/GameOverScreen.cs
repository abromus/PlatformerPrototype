namespace PlatformerPrototype.Game.Services
{
    internal sealed class GameOverScreen : BaseScreen
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonRestart;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonExit;

        private Data.IGameData _gameData;
        private bool _isShown;

        public override Configs.ScreenType ScreenType => Configs.ScreenType.GameOver;

        public override bool IsShown => _isShown;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Init(Data.IGameData gameData, in IScreenArgs args = null)
        {
            _gameData = gameData;
        }

        public override void Show()
        {
            base.Show();

            Subscribe();

            _isShown = true;
        }

        public override void Hide()
        {
            base.Hide();

            Unsubscribe();

            _isShown = false;
        }

        private void Subscribe()
        {
            _buttonRestart.onClick.AddListener(OnButtonRestartClicked);
            _buttonExit.onClick.AddListener(OnButtonExitClicked);
        }

        private void Unsubscribe()
        {
            _buttonRestart.onClick.RemoveListener(OnButtonRestartClicked);
            _buttonExit.onClick.RemoveListener(OnButtonExitClicked);
        }

        private void OnButtonRestartClicked()
        {
            _gameData.Restart();

            Hide();
        }

        private void OnButtonExitClicked()
        {
            _gameData.Exit();
        }
    }
}
