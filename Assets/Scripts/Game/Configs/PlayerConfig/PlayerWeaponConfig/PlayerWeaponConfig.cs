namespace PlatformerPrototype.Game.Configs
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(PlayerWeaponConfig), menuName = ConfigKeys.GamePathKey + nameof(PlayerWeaponConfig))]
    internal sealed class PlayerWeaponConfig : UnityEngine.ScriptableObject, IPlayerWeaponConfig
    {
        [UnityEngine.SerializeField] private WeaponInfo[] _weaponInfos;

        public WeaponInfo[] WeaponInfos => _weaponInfos;
    }
}
