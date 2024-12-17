namespace PlatformerPrototype.Game.World.Entities
{
    internal interface IPlayerDropConsumer
    {
        public void Apply(Drops.IDrop drop);
    }
}
