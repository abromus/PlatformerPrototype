namespace PlatformerPrototype.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(ScreensConfig), menuName = ConfigKeys.GamePathKey + nameof(ScreensConfig))]
    internal sealed class ScreensConfig : UnityEngine.ScriptableObject, IScreensConfig
    {
        [UnityEngine.SerializeField] private ScreenInfo[] _screenInfos;

        public ScreenInfo[] ScreenInfos => _screenInfos;
    }
}
