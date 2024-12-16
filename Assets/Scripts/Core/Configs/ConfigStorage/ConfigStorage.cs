namespace PlatformerPrototype.Core.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(ConfigStorage), menuName = ConfigKeys.CorePathKey + nameof(ConfigStorage))]
    internal sealed class ConfigStorage : UnityEngine.ScriptableObject, IConfigStorage
    {
        [UnityEngine.SerializeField] private UiServiceConfig _uiServiceConfig;
        [UnityEngine.SerializeField] private UiFactoryConfig _uiFactoryConfig;

        private System.Collections.Generic.Dictionary<System.Type, IConfig> _configs;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Init()
        {
            _configs = new(8)
            {
                [typeof(IUiServiceConfig)] = _uiServiceConfig,
                [typeof(IUiFactoryConfig)] = _uiFactoryConfig,
            };
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public TConfig GetConfig<TConfig>() where TConfig : class, IConfig
        {
            return _configs[typeof(TConfig)] as TConfig;
        }
    }
}
