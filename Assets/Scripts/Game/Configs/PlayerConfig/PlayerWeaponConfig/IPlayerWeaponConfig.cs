namespace PlatformerPrototype.Game.Configs
{
    internal interface IPlayerWeaponConfig : Core.Configs.IConfig
    {
        public WeaponInfo[] WeaponInfos { get; }
    }
}
