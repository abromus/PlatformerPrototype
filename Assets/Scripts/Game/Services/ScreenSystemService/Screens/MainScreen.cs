namespace PlatformerPrototype.Game.Services
{
    internal sealed class MainScreen : BaseScreen
    {
        [UnityEngine.SerializeField] private TMPro.TMP_Text _ammoView;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonSettings;

        private Data.IGameData _gameData;
        private World.Entities.IPlayer _player;
        private IScreenSystemService _screenSystemService;
        private bool _isShown;

        public override Configs.ScreenType ScreenType => Configs.ScreenType.Main;

        public override bool IsShown => _isShown;

        public override void Init(Data.IGameData gameData, in IScreenArgs args = null)
        {
            _gameData = gameData;
            _player = ((MainScreenArgs)args).Player;
            _screenSystemService = _gameData.ServiceStorage.GetService<IScreenSystemService>();
        }

        public override void Show()
        {
            base.Show();

            UpdateView();
            Subscribe();

            _isShown = true;
        }

        public override void Hide()
        {
            base.Hide();

            Unsubscribe();

            _isShown = false;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void UpdateView()
        {
            _ammoView.text = $"{_player.CurrentAmmo}";
        }

        private void Subscribe()
        {
            _buttonSettings.onClick.AddListener(OnButtonSettingsClicked);

            _player.AmmoChanged += OnAmmoChanged;
        }

        private void Unsubscribe()
        {
            _buttonSettings.onClick.RemoveListener(OnButtonSettingsClicked);

            if (_player != null)
                _player.AmmoChanged += OnAmmoChanged;
        }

        private void OnButtonSettingsClicked()
        {
            _screenSystemService.Show(Configs.ScreenType.Settings);
        }

        private void OnAmmoChanged()
        {
            UpdateView();
        }
    }
}
