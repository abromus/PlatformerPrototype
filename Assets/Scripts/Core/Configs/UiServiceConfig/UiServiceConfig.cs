namespace PlatformerPrototype.Core.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(UiServiceConfig), menuName = ConfigKeys.CorePathKey + nameof(UiServiceConfig))]
    public sealed class UiServiceConfig : UnityEngine.ScriptableObject, IUiServiceConfig
    {
        [UnityEngine.SerializeField] private Services.BaseUiService[] _uiServices;

        public Services.IUiService[] UiServices => _uiServices;
    }
}
