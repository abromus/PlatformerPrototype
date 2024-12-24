namespace PlatformerPrototype.Game.World.Entities
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct EnemyArgs
    {
        private readonly Services.IAudioService _audioService;
        private readonly UnityEngine.Transform _player;
        private readonly Configs.EnemyInfo _info;

        internal readonly Services.IAudioService AudioService => _audioService;

        internal readonly UnityEngine.Transform Player => _player;

        internal readonly Configs.EnemyInfo Info => _info;

        internal EnemyArgs(
            Services.IAudioService audioService,
            UnityEngine.Transform player,
            Configs.EnemyInfo info)
        {
            _audioService = audioService;
            _player = player;
            _info = info;
        }
    }
}
