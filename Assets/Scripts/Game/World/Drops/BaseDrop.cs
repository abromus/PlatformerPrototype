﻿namespace PlatformerPrototype.Game.World.Drops
{
    internal abstract class BaseDrop : UnityEngine.MonoBehaviour, IDrop
    {
        public abstract Configs.DropType DropType { get; }

        public abstract int Count { get; }

        public abstract event System.Action<IDrop> Destroyed;

        public abstract void Init(Services.IAudioService audioService, Configs.IDropConfig config);

        public abstract void InitPosition(in UnityEngine.Vector3 position);

        public abstract void Apply();

        public abstract void Activate();

        public abstract void Deactivate();

        public abstract void Clear();

        public abstract void Destroy();
    }
}
