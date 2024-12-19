namespace PlatformerPrototype.Game.Services
{
    internal sealed class ScreenSystemService : Core.Services.BaseUiService, IScreenSystemService
    {
        [UnityEngine.SerializeField] private UnityEngine.Transform _screensContainer;

        private Data.IGameData _gameData;
        private Configs.ScreenInfo[] _screenInfos;

        private readonly System.Collections.Generic.Dictionary<Configs.ScreenType, BaseScreen> _screens = new(4);

        public void Init(Data.IGameData gameData)
        {
            _gameData = gameData;
            _screenInfos = _gameData.ConfigStorage.GetConfig<Configs.IScreensConfig>().ScreenInfos;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void AttachTo(UnityEngine.Transform parent)
        {
            transform.SetParent(parent);
        }

        public void Show(Configs.ScreenType screenType, in IScreenArgs args = null)
        {
            if (TryShowFromCache(screenType))
                return;

            for (int i = 0; i < _screenInfos.Length; i++)
            {
                var screenInfo = _screenInfos[i];

                if (screenInfo.ScreenType != screenType)
                    continue;

                var screenPrefab = screenInfo.Screen;
                var screen = Instantiate(screenPrefab, _screensContainer);
                screen.Init(_gameData, in args);
                screen.Show();

                _screens.Add(screenType, screen);

                return;
            }

#if UNITY_EDITOR
            UnityEngine.Debug.LogError($"[ScreenSystemService] Hasn't screen with type {screenType}");
#endif
        }

        public void Hide(Configs.ScreenType screenType)
        {
            if (_screens.ContainsKey(screenType))
                _screens[screenType].Hide();
        }

        public void HideScreens()
        {
            var screens = _screens.Values;

            foreach (var screen in screens)
                screen.Hide();
        }

        public override void Destroy()
        {
            var screens = _screens.Values;

            foreach (var screen in screens)
                if (screen != null && screen.gameObject != null)
                    Destroy(screen.gameObject);

            _screens.Clear();
        }

        private bool TryShowFromCache(Configs.ScreenType screenType)
        {
            if (_screens.ContainsKey(screenType) == false)
                return false;

            var screen = _screens[screenType];

            if (screen.IsShown)
                return true;

            screen.Show();

            return true;
        }
    }
}
