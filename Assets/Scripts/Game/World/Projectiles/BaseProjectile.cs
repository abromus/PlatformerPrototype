namespace PlatformerPrototype.Game.World.Projectiles
{
    internal abstract class BaseProjectile : UnityEngine.MonoBehaviour, IProjectile
    {
        public abstract float Damage { get; }

        public abstract event System.Action<IProjectile> Destroyed;

        public abstract void Init(Core.Services.IUpdaterService updaterSevice);

        public abstract void InitPosition(UnityEngine.Vector3 position, float direction);

        public abstract void Tick(float deltaTime);

        public abstract void SetPause(bool isPaused);

        public abstract void Activate();

        public abstract void Deactivate();

        public abstract void Clear();

        public abstract void Destroy();
    }
}
