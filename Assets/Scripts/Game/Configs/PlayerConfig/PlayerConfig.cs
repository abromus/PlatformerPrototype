namespace PlatformerPrototype.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = ConfigKeys.GamePathKey + nameof(PlayerConfig))]
    internal sealed class PlayerConfig : UnityEngine.ScriptableObject, IPlayerConfig
    {
        [UnityEngine.SerializeField] private UnityEngine.Vector2 _movementSensitivity;

        public UnityEngine.Vector2 MovementSensitivity => _movementSensitivity;
    }
}
