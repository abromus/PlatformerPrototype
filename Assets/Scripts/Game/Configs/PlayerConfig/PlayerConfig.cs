namespace PlatformerPrototype.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = ConfigKeys.GamePathKey + nameof(PlayerConfig))]
    internal sealed class PlayerConfig : UnityEngine.ScriptableObject, IPlayerConfig
    {
        [UnityEngine.SerializeField] private float _movementXSensitivity;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private float _hp;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private PlayerWeaponConfig _weaponConfig;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _deathClip;

        public float MovementXSensitivity => _movementXSensitivity;

        public float Hp => _hp;

        public IPlayerWeaponConfig WeaponConfig => _weaponConfig;

        public UnityEngine.AudioClip DeathClip => _deathClip;
    }
}
