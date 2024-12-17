namespace PlatformerPrototype.Game.Configs
{
    internal interface IPlayerConfig : Core.Configs.IConfig
    {
        public float MovementXSensitivity { get; }

        public float Hp { get; }

        public IPlayerWeaponConfig WeaponConfig { get; }
    }
}
