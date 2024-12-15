﻿namespace PlatformerPrototype.Game.Services
{
    internal sealed class GameLoopState : Core.Services.IEnterState<GameStateArgs>
    {
        private readonly Core.Services.IStateMachine _stateMachine;

        internal GameLoopState(Core.Services.IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Enter(GameStateArgs args)
        {
            //_sceneLoader.Load(in info);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}