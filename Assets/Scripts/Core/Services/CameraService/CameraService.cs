namespace PlatformerPrototype.Core.Services
{
    internal sealed class CameraService : BaseUiService, ICameraService
    {
        [UnityEngine.SerializeField] private UnityEngine.Camera _cameraPrefab;

        private UnityEngine.Transform _container;
        private UnityEngine.Camera _camera;

        private readonly UnityEngine.Vector3[] _frustumCorners = new UnityEngine.Vector3[4];
        private readonly UnityEngine.Rect _viewport = new(0, 0, 1, 1);

        public UnityEngine.Transform CameraTransform => _camera.transform;

        public void Init(UnityEngine.Transform container)
        {
            _container = container;
            _camera = Instantiate(_cameraPrefab, container);
        }

        public void AttachTo(UnityEngine.Transform parent)
        {
            if (_camera == null)
                return;

            _camera.transform.SetParent(parent);
        }

        public void Detach()
        {
            if (_camera == null)
                return;

            _camera.transform.SetParent(_container);
        }

        public UnityEngine.Rect GetScreenRect()
        {
            _camera.CalculateFrustumCorners(
                _viewport,
                _camera.nearClipPlane - _camera.transform.position.z,
                UnityEngine.Camera.MonoOrStereoscopicEye.Mono,
                _frustumCorners);

            var screenRect = new UnityEngine.Rect(
                0f,
                0f,
                _frustumCorners[2].x - _frustumCorners[0].x,
                _frustumCorners[2].y - _frustumCorners[0].y);

            return screenRect;
        }

        public override void Destroy()
        {
            if (_camera != null && _camera.gameObject != null)
                Destroy(_camera.gameObject);
        }
    }
}
