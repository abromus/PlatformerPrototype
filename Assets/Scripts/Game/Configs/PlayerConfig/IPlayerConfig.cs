namespace PlatformerPrototype.Game.Configs
{
    internal interface IPlayerConfig : Core.Configs.IConfig
    {
        public UnityEngine.Vector2 MovementSensitivity { get; }
    }
}
