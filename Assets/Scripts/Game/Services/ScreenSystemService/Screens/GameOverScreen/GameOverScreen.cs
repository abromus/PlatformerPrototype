namespace PlatformerPrototype.Game.Services
{
    internal sealed class GameOverScreen : BaseScreen
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonRestart;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonExit;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _backgroundMusic;

        private Data.IGameData _gameData;
        private IAudioService _audioService;
        private bool _isShown;

        public override Configs.ScreenType ScreenType => Configs.ScreenType.GameOver;

        public override bool IsShown => _isShown;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Init(Data.IGameData gameData, in IScreenArgs args = null)
        {
            _gameData = gameData;

            if (args == null)
                UnityEngine.Debug.LogError($"args is null!");

            if (args is GameOverScreenArgs gameOverScreenArgs)
                _audioService = gameOverScreenArgs.AudioService;
            else
                UnityEngine.Debug.LogError($"args is not {typeof(GameOverScreenArgs)}!");
        }

        public override void Show()
        {
            base.Show();

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
            Hide();

            _gameData.Restart();
        }

        private void OnButtonExitClicked()
        {
            _gameData.Exit();
        }
    }
}
