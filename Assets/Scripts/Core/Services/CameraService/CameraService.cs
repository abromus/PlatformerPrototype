namespace PlatformerPrototype.Core.Services
{
    internal sealed class CameraService : BaseUiService, ICameraService
    {
        [UnityEngine.SerializeField] private UnityEngine.Camera _cameraPrefab;

        private UnityEngine.Transform _container;
        private UnityEngine.Camera _camera;

        public UnityEngine.Camera Camera => _camera;

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
            var bottomLeft = _camera.ScreenToWorldPoint(UnityEngine.Vector3.zero);
            var topRight = _camera.ScreenToWorldPoint(new UnityEngine.Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, _camera.nearClipPlane));
            var screenRect = new UnityEngine.Rect(bottomLeft.x, bottomLeft.y, topRight.x, topRight.y);

            return screenRect;
        }

        public override void Destroy()
        {
            if (_camera != null && _camera.gameObject != null)
                Destroy(_camera.gameObject);
        }
    }
}
