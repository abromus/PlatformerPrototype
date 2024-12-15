namespace PlatformerPrototype.Game.Configs
{
    internal interface IPlayerConfig : Core.Configs.IConfig
    {
        public UnityEngine.Vector2 MovementSensitivity { get; }

        public float SingleShootingDelay { get; }

        public float ContinuousShootingDelay { get; }

        public UnityEngine.Vector3 ProjectileOffset { get; }
    }
}
