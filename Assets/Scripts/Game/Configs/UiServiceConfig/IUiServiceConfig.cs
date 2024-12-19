namespace PlatformerPrototype.Game.Configs
{
    public interface IUiServiceConfig : Core.Configs.IConfig
    {
        public Core.Services.IUiService[] UiServices { get; }
    }
}
