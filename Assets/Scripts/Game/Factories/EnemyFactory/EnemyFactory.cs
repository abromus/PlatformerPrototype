using PlatformerPrototype.Core;

namespace PlatformerPrototype.Game.Factories
{
    internal sealed class EnemyFactory : Core.Factories.BaseUiFactory, IEnemyFactory
    {
        public TEnemy Create<TEnemy>(TEnemy enemyPrefab, UnityEngine.Transform container) where TEnemy : World.Entities.BaseEnemy
        {
            var enemy = Instantiate(enemyPrefab, container);
            enemy.gameObject.RemoveCloneSuffix();

            return enemy;
        }
    }
}
