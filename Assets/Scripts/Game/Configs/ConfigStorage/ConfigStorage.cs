namespace PlatformerPrototype.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(ConfigStorage), menuName = ConfigKeys.GamePathKey + nameof(ConfigStorage))]
    internal sealed class ConfigStorage : UnityEngine.ScriptableObject, Core.Configs.IConfigStorage
    {
        [UnityEngine.SerializeField] private Core.Configs.UiFactoryConfig _uiFactoryConfig;

        private System.Collections.Generic.Dictionary<System.Type, Core.Configs.IConfig> _configs;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Init()
        {
            _configs = new(8)
            {
                [typeof(Core.Configs.IUiFactoryConfig)] = _uiFactoryConfig,
            };
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public TConfig GetConfig<TConfig>() where TConfig : class, Core.Configs.IConfig
        {
            return _configs[typeof(TConfig)] as TConfig;
        }
    }
}
