namespace PlatformerPrototype.Core.Configs
{
    internal interface IUiFactoryConfig : IConfig
    {
        public Factories.IUiFactory[] UiFactories { get; }
    }
}
