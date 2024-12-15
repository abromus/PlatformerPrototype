namespace PlatformerPrototype.Core.Services
{
    internal interface ISceneLoader : IService
    {
        public void Load(in SceneInfo info);
    }
}
