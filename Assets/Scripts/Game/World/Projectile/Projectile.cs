namespace PlatformerPrototype.Game.World.Projectiles
{
    internal sealed class Projectile : BaseProjectile
    {
        [UnityEngine.SerializeField] private float _speed;
        [UnityEngine.SerializeField] private float _existenceTime;

        private Core.Services.IUpdaterService _updaterSevice;
        private UnityEngine.Vector3 _direction;
        private float _movingTime;
        private bool _isPaused;

        public override event System.Action<IProjectile> Destroyed;

        public override void Init(Core.Services.IUpdaterService updaterSevice)
        {
            _updaterSevice = updaterSevice;
        }

        public override void InitPosition(UnityEngine.Vector3 position, float direction)
        {
            transform.position = position;

            _direction.x = direction;
            _movingTime = 0f;

            gameObject.SetActive(true);

            Subscribe();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
        }

        public override void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        public override void Clear()
        {
            Destroy();

            //отключить звук
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void Move(float deltaTime)
        {
            if (_isPaused)
                return;

            _movingTime += deltaTime;

            if (_movingTime < _existenceTime)
                transform.position += _speed * deltaTime * _direction;
            else
                Destroyed?.Invoke(this);
        }

        private void Destroy()
        {
            Unsubscribe();

            gameObject.SetActive(false);
        }

        private void Subscribe()
        {
            _updaterSevice.AddUpdatable(this);
            _updaterSevice.AddPausable(this);
        }

        private void Unsubscribe()
        {
            _updaterSevice.RemoveUpdatable(this);
            _updaterSevice.RemovePausable(this);
        }
    }
}
