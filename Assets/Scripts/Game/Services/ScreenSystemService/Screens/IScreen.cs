namespace PlatformerPrototype.Game.Services
{
    internal interface IScreen
    {
        public Configs.ScreenType ScreenType { get; }

        public bool IsShown { get; }

        public void Init(Data.IGameData gameData, in IScreenArgs args = null);

        public void Show();

        public void Hide();
    }
}
