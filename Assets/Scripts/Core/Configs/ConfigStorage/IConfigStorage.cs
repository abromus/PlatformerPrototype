namespace PlatformerPrototype.Core.Configs
{
    internal interface IConfigStorage
    {
        public void Init();

        public TConfig GetConfig<TConfig>() where TConfig : class, IConfig;
    }
}
