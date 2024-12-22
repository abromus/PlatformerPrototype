namespace PlatformerPrototype.Game.World
{
    internal interface IWorld :
        Core.Services.IUpdatable,
        Core.Services.IFixedUpdatable,
        Core.Services.ILateUpdatable,
        Core.Services.IPausable,
        Core.IDestroyable,
        IRestartable
    {
        public void Init(Data.IGameData gameData, Core.Services.IStateMachine stateMachine);
    }
}
