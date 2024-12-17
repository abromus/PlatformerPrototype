﻿namespace PlatformerPrototype.Game.World.Drops
{
    internal interface IDrop : Core.IPoolable
    {
        public Configs.DropType DropType { get; }

        public int Count { get; }

        public event System.Action<IDrop> Destroyed;

        public void Init(Configs.DropType dropType, int minCount, int maxCount);

        public void InitPosition(UnityEngine.Vector3 position);

        public void Apply();
    }
}
