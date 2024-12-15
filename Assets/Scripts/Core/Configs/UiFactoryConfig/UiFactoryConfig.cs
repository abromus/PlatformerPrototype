namespace PlatformerPrototype.Core.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(UiFactoryConfig), menuName = ConfigKeys.CorePathKey + nameof(UiFactoryConfig))]
    public sealed class UiFactoryConfig : UnityEngine.ScriptableObject, IUiFactoryConfig
    {
        [UnityEngine.SerializeField] private Factories.BaseUiFactory[] _uiFactories;

        public Factories.IUiFactory[] UiFactories => _uiFactories;
    }
}
