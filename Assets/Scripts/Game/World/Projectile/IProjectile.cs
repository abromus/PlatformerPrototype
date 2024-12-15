﻿namespace PlatformerPrototype.Game.World.Projectiles
{
    internal interface IProjectile : Core.IPoolable, Core.Services.IUpdatable, Core.Services.IPausable
    {
        public event System.Action<IProjectile> Destroyed;

        public void Init(Core.Services.IUpdaterService updaterSevice);

        public void InitPosition(UnityEngine.Vector3 position, float direction);

        public void Clear();
    }
}