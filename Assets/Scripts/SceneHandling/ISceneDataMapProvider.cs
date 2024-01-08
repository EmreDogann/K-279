using System.Collections.Generic;

namespace SceneHandling
{
    // From: https://stackoverflow.com/a/66927384
    public interface ISceneDataMapProvider<T> where T : ISceneDataMapProvider<T>
    {
        public string SavedFilePath { get; }
        internal void DirectAssign(Dictionary<string, object> map);
        internal ISceneDataMapGenerator GetGenerator();
    }

    internal static class SceneDataMapProviderSurrogate<T> where T : ISceneDataMapProvider<T>, new()
    {
        private static readonly T value = new T();

        public static string SavedFilePath => value.SavedFilePath;

        public static void DirectAssign(Dictionary<string, object> map)
        {
            value.DirectAssign(map);
        }

        public static ISceneDataMapGenerator GetGenerator()
        {
            return value.GetGenerator();
        }
    }
}