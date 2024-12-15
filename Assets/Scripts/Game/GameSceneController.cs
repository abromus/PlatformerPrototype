namespace PlatformerPrototype.Game
{
    internal sealed class GameSceneController : Core.BaseSceneController
    {
        //[UnityEngine.SerializeField] private Configs.ConfigStorage _configStorage;

        //private Data.ICoreData _coreData;

        /*[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal void CreateCoreData()
        {
            _coreData = new Data.CoreData(this);

            EnterInitState();
        }*/

        public override void Init()
        {
            UnityEngine.Debug.Log($"Init");

        }

        public override void Run()
        {
            UnityEngine.Debug.Log($"Run");

        }

        public override void Destroy()
        {
            UnityEngine.Debug.Log($"Destroy");

        }

        /*private void EnterInitState()
        {
            var stateMachine = _coreData.ServiceStorage.GetService<Services.IStateMachine>();

            stateMachine.Enter<Services.BootstrapState>();
        }*/
    }
}
