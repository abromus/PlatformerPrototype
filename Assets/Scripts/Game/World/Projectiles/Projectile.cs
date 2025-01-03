﻿namespace PlatformerPrototype.Game.World.Projectiles
{
    [System.Serializable]
    internal sealed class Projectile : BaseProjectile
    {
        [UnityEngine.SerializeField] private float _damage;
        [UnityEngine.SerializeField] private float _speed;
        [UnityEngine.SerializeField] private float _existenceTime;

        private Core.Services.IUpdaterService _updaterService;
        private UnityEngine.Vector3 _direction;
        private float _movingTime;
        private bool _isPaused;
        private bool _isDestroyed;

        public override float Damage => _damage;

        public override event System.Action<IProjectile> Destroyed;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Init(Core.Services.IUpdaterService updaterSevice)
        {
            _updaterService = updaterSevice;
        }

        public override void InitPosition(in UnityEngine.Vector3 position, float direction)
        {
            transform.position = position;

            _direction.x = direction;
        }

        public override void Activate()
        {
            _isDestroyed = false;
            _movingTime = 0f;

            gameObject.SetActive(true);

            Subscribe();
        }

        public override void Deactivate()
        {
            _isDestroyed = true;
            _movingTime = 0f;

            gameObject.SetActive(false);

            Unsubscribe();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Clear()
        {
            Deactivate();
        }

        public override void Destroy()
        {
            Unsubscribe();

            gameObject.SetActive(false);

            _isDestroyed = true;
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            if (_isDestroyed)
                return;

            Destroyed?.Invoke(this);
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

        private void Subscribe()
        {
            _updaterService.AddUpdatable(this);
            _updaterService.AddPausable(this);
        }

        private void Unsubscribe()
        {
            if (_updaterService != null)
            {
                _updaterService.RemoveUpdatable(this);
                _updaterService.RemovePausable(this);
            }
        }
    }
}
