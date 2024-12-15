namespace PlatformerPrototype.Core
{
    internal sealed class CoreSceneController : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private Configs.ConfigStorage _configStorage;

        private Data.ICoreData _coreData;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void Init()
        {
            _configStorage.Init();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void CreateCoreData()
        {
            _coreData = new Data.CoreData(this, _configStorage);

            EnterInitState();
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
