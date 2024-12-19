namespace PlatformerPrototype.Game.Services
{
    internal sealed class SettingsScreen : BaseScreen
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonRestart;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonExit;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonApply;

        private Data.IGameData _gameData;
        private Core.Services.IUpdaterService _updaterService;
        private bool _isShown;

        public override Configs.ScreenType ScreenType => Configs.ScreenType.GameOver;

        public override bool IsShown => _isShown;

        public override void Init(Data.IGameData gameData, in IScreenArgs args = null)
        {
            _gameData = gameData;

            _updaterService = _gameData.CoreData.ServiceStorage.GetService<Core.Services.IUpdaterService>();
        }

        public override void Show()
        {
            base.Show();

            _updaterService.SetPause(true);

            Subscribe();

            _isShown = true;
        }

        public override void Hide()
        {
            base.Hide();

            Unsubscribe();

            _updaterService.SetPause(false);

            _isShown = false;
        }

        private void Subscribe()
        {
            _buttonRestart.onClick.AddListener(OnButtonRestartClicked);
            _buttonExit.onClick.AddListener(OnButtonExitClicked);
            _buttonApply.onClick.AddListener(OnButtonApplyClicked);
        }

        private void Unsubscribe()
        {
            _buttonRestart.onClick.RemoveListener(OnButtonRestartClicked);
            _buttonExit.onClick.RemoveListener(OnButtonExitClicked);
            _buttonApply.onClick.RemoveListener(OnButtonApplyClicked);
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

        private void OnButtonApplyClicked()
        {
            Hide();
        }
    }
}
