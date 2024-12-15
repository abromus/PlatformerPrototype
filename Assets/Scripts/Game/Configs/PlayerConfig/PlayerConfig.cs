namespace PlatformerPrototype.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = ConfigKeys.GamePathKey + nameof(PlayerConfig))]
    internal sealed class PlayerConfig : UnityEngine.ScriptableObject, IPlayerConfig
    {
        [UnityEngine.SerializeField] private float _movementXSensitivity;
        [UnityEngine.Space]
        //Если появятся виды оружия, то вынести в отдельный конфиг
        [UnityEngine.SerializeField] private float _singleShootingDelay = .5f;
        [UnityEngine.SerializeField] private float _continuousShootingDelay = 0.25f;
        [UnityEngine.SerializeField] private UnityEngine.Vector3 _projectileOffset;

        public float MovementXSensitivity => _movementXSensitivity;

        public float SingleShootingDelay => _singleShootingDelay;

        public float ContinuousShootingDelay => _continuousShootingDelay;

        public UnityEngine.Vector3 ProjectileOffset => _projectileOffset;
    }
}
