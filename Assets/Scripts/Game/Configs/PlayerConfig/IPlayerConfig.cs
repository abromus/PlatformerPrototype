namespace PlatformerPrototype.Game.Configs
{
    internal interface IPlayerConfig : Core.Configs.IConfig
    {
        public float MovementXSensitivity { get; }

        public float Hp { get; }

        public float SingleShootingDelay { get; }

        public float ContinuousShootingDelay { get; }

        public UnityEngine.Vector3 ProjectileOffset { get; }
    }
}
