namespace PlatformerPrototype.Game.Services
{
    internal sealed class MainScreen : BaseScreen
    {
        [UnityEngine.SerializeField] private TMPro.TMP_Text _ammoView;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonSettings;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _backgroundMusic;

        private Data.IGameData _gameData;
        private IAudioService _audioService;
        private World.Entities.IPlayer _player;
        private IScreenSystemService _screenSystemService;
        private bool _isShown;

        public override Configs.ScreenType ScreenType => Configs.ScreenType.Main;

        public override bool IsShown => _isShown;

        public override void Init(Data.IGameData gameData, in IScreenArgs args = null)
        {
            _gameData = gameData;

            if (args == null)
                UnityEngine.Debug.LogError($"args is null!");

            var mainScreenArgs = (MainScreenArgs)args;
            _audioService = mainScreenArgs.AudioService;
            _player = mainScreenArgs.Player;
            _screenSystemService = _gameData.ServiceStorage.GetService<IScreenSystemService>();
        }

        public override void Show()
        {
            base.Show();

            UpdateView();
            PlayBackgroundMusic();
            Subscribe();

            _isShown = true;
        }

        public override void Hide()
        {
            base.Hide();

            StopBackgroundMusic();
            Unsubscribe();

            _isShown = false;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void UpdateView()
        {
            _ammoView.text = $"{_player.CurrentAmmo}";
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void PlayBackgroundMusic()
        {
            _audioService.PlayBackgroundMusic(_backgroundMusic);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void StopBackgroundMusic()
        {
            _audioService.StopBackgroundMusic();
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
