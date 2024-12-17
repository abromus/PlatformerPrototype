namespace PlatformerPrototype.Game.World.Entities
{
    internal sealed class PlayerDropConsumer : IPlayerDropConsumer
    {
        private readonly IPlayerShooting _shooting;

        internal PlayerDropConsumer(IPlayerShooting shooting)
        {
            _shooting = shooting;
        }

        public void Apply(Drops.IDrop drop)
        {
            var dropType = drop.DropType;

            switch (dropType)
            {
                case Configs.DropType.Ammo:
                case Configs.DropType.BigAmmo:
                    _shooting.AddAmmo(drop.Count);
                    break;
                default:
                    break;
            }
        }
    }
}
