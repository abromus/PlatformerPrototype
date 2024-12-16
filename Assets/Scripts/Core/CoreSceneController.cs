namespace PlatformerPrototype.Core
{
    internal sealed class CoreSceneController : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private Configs.ConfigStorage _configStorage;
        [UnityEngine.SerializeField] private UnityEngine.Transform _uiServiceContainer;

        private Data.ICoreData _coreData;
        private Services.IUpdaterService _updater;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void Init()
        {
            _configStorage.Init();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void CreateCoreData()
        {
            _coreData = new Data.CoreData(this, _configStorage, _uiServiceContainer);

            _updater = _coreData.ServiceStorage.GetService<Services.IUpdaterService>();

            EnterInitState();
        }

        private void Update()
        {
            _updater.Tick(UnityEngine.Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _updater.FixedTick(UnityEngine.Time.deltaTime);
        }

        private void LateUpdate()
        {
            _updater.LateTick(UnityEngine.Time.deltaTime);
        }

        private void OnApplicationFocus(bool focus)
        {
            _updater.SetPause(focus == false);
        }

        private void OnApplicationPause(bool pause)
        {
            _updater.SetPause(pause);
        }

        private void OnDestroy()
        {
            _coreData.Destroy();
        }

        private void EnterInitState()
        {
            var stateMachine = _coreData.ServiceStorage.GetService<Services.IStateMachine>();

            stateMachine.Enter<Services.BootstrapState>();
        }
    }
}
