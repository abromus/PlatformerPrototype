namespace PlatformerPrototype.Core.Configs
{
    public interface IConfigStorage
    {
        public void Init();

        public TConfig GetConfig<TConfig>() where TConfig : class, IConfig;
    }
}
