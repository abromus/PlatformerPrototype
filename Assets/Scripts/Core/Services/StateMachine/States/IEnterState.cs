﻿namespace PlatformerPrototype.Core.Services
{
    public interface IEnterState : IExitState
    {
        public void Enter();
    }

    public interface IEnterState<TPayload> : IExitState
    {
        public void Enter(in TPayload payload);
    }
}
