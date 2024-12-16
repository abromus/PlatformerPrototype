namespace PlatformerPrototype.Core.Configs
{
    public interface IUiServiceConfig : IConfig
    {
        public Services.IUiService[] UiServices { get; }
    }
}
