namespace PlatformerPrototype.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(UiServiceConfig), menuName = ConfigKeys.GamePathKey + nameof(UiServiceConfig))]
    public sealed class UiServiceConfig : UnityEngine.ScriptableObject, IUiServiceConfig
    {
        [UnityEngine.SerializeField] private Core.Services.BaseUiService[] _uiServices;

        public Core.Services.IUiService[] UiServices => _uiServices;
    }
}
