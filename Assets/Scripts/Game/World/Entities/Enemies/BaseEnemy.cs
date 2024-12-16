namespace PlatformerPrototype.Game.World.Entities
{
    internal abstract class BaseEnemy : UnityEngine.MonoBehaviour, IEnemy
    {
        [UnityEngine.SerializeField] private UnityEngine.Animator _animator;

        public abstract int Index { get; }

        public abstract UnityEngine.Vector2 Size { get; }

        public abstract event System.Action<IEnemy> Dead;

        public abstract void Init(in EnemyArgs args);

        public abstract void InitPosition(UnityEngine.Vector3 position);

        public abstract void InitHp();

        public abstract void Activate();

        public abstract void FixedTick(float deltaTime);

        public abstract void SetPause(bool isPaused);

        public abstract void Clear();

        public abstract void SetIndex(int index);

        public abstract void Destroy();

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEngine.Gizmos.color = UnityEngine.Color.yellow;
            UnityEngine.Gizmos.DrawWireCube(transform.position, Size);
        }
#endif
    }
}
