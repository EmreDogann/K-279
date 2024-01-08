using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace SceneHandling
{
    /// <summary>
    ///     Generates and writes the scene data maps.
    /// </summary>
    [PublicAPI]
    public static class SceneDataMapsGenerator
    {
        /// <summary>
        ///     Runs the generator.
        /// </summary>
        /// <remarks>Does nothing in build.</remarks>
        [MenuItem("Tools/Scene Manager/Generate Scene Data Maps", priority = -3130)]
        public static void TestRun()
        {
            Run<SceneGuidToPathMapProvider>();
            Run<ManagedSceneToRefMapProvider>();
        }

        public static void Run<T>() where T : ISceneDataMapProvider<T>, new()
        {
#if UNITY_EDITOR
            try
            {
                Debug.Log("Re-generating scene data maps...");

                EnsureScaffolding<T>();

                var sceneGuidToPathMap = SceneDataMapProviderSurrogate<T>.GetGenerator().GenerateDataMap();
                WriteSceneGuidToPathMap<T>(sceneGuidToPathMap);
            }
            finally
            {
                AssetDatabase.Refresh();
            }
#endif
        }

#if UNITY_EDITOR
        internal static void EnsureScaffolding<T>() where T : ISceneDataMapProvider<T>, new()
        {
            if (!File.Exists(SceneDataMapProviderSurrogate<T>.SavedFilePath))
            {
                File.WriteAllText(SceneDataMapProviderSurrogate<T>.SavedFilePath, "{}");
            }
        }

        private static void WriteSceneGuidToPathMap<T>(Dictionary<string, object> sceneGuidToPathMap)
            where T : ISceneDataMapProvider<T>, new()
        {
            string jsonRaw = JsonConvert.SerializeObject(sceneGuidToPathMap, Formatting.Indented);
            File.WriteAllText(SceneDataMapProviderSurrogate<T>.SavedFilePath, jsonRaw);

            SceneDataMapProviderSurrogate<T>.DirectAssign(sceneGuidToPathMap);
        }
#endif
    }
}