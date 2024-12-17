using PlatformerPrototype.Core;

namespace PlatformerPrototype.Game.Factories
{
    internal sealed class DropFactory : Core.Factories.BaseUiFactory, IDropFactory
    {
        public World.Drops.IDrop Create(Configs.IDropConfig dropConfig, UnityEngine.Transform container)
        {
            var dropPrefab = dropConfig.BaseDropPrefab;
            var drop = Instantiate(dropPrefab, container);
            drop.gameObject.RemoveCloneSuffix();
            drop.Init(dropConfig.DropType, dropConfig.MinCount, dropConfig.MaxCount);

            return drop;
        }
    }
}
