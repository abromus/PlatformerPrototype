namespace PlatformerPrototype.Game.Factories
{
    internal interface IEnemyFactory : Core.Factories.IFactory
    {
        public TEnemy Create<TEnemy>(TEnemy enemyPrefab, UnityEngine.Transform container) where TEnemy : World.Entities.BaseEnemy;
    }
}
