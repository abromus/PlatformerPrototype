namespace PlatformerPrototype.Game.Configs
{
    [System.Serializable]
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 4)]
    internal struct WeaponInfo
    {
        [UnityEngine.SerializeField] private float _singleShootingDelay;
        [UnityEngine.SerializeField] private float _continuousShootingDelay;
        [UnityEngine.SerializeField] private UnityEngine.Vector3 _projectileOffset;
        [UnityEngine.SerializeField] private int _capacity;
        [UnityEngine.SerializeField] private World.Projectiles.Projectile _projectilePrefab;

        internal readonly float SingleShootingDelay => _singleShootingDelay;

        internal readonly float ContinuousShootingDelay => _continuousShootingDelay;

        internal readonly UnityEngine.Vector3 ProjectileOffset => _projectileOffset;

        internal readonly int Capacity => _capacity;

        internal readonly World.Projectiles.Projectile ProjectilePrefab => _projectilePrefab;
    }
}
