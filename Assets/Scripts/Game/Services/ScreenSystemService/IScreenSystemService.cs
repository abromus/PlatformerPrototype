namespace PlatformerPrototype.Game.Services
{
    internal interface IScreenSystemService : Core.Services.IUiService
    {
        public void Init(Data.IGameData gameData);

        public void AttachTo(UnityEngine.Transform parent);

        public void Show(Configs.ScreenType screenType, in IScreenArgs args = null);

        public void Hide(Configs.ScreenType screenType);

        public void HideScreens();
    }
}
