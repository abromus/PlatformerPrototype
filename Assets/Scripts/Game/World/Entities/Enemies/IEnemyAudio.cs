namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IEnemyAudio
    {
        public void PlayRunningClip();

        public void PlayDeathClip();

        public void StopLoopClips();
    }
}
