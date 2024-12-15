namespace PlatformerPrototype.Core
{
    public static class UnityUtils
    {
        public static void RemoveCloneSuffix(this UnityEngine.GameObject gameObject)
        {
            if (gameObject == null)
                return;

            var name = gameObject.name.Replace(Keys.CloneSuffix, Keys.Empty);
            gameObject.name = name;
        }

        private sealed class Keys
        {
            internal const string Empty = "";
            internal const string CloneSuffix = "(Clone)";
        }
    }
}
