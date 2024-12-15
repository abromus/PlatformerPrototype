namespace PlatformerPrototype.Core.Services
{
    public interface ILateUpdatable
    {
        public void LateTick(float deltaTime);
    }
}
