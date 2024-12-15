namespace PlatformerPrototype.Core.Configs
{
    public interface IUiFactoryConfig : IConfig
    {
        public Factories.IUiFactory[] UiFactories { get; }
    }
}
