using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        // [MenuItem("Tools/" + Constants.MenuPrefixBase + "/Generate Scene Data Maps", priority = -3130)]
        public static void Run()
        {
#if UNITY_EDITOR
            try
            {
                Debug.Log("Re-generating scene data maps.");

                EnsureScaffolding();

                string[] allSceneGuids = AssetDatabase.FindAssets("t:Scene");

                var sceneGuidToPathMap = GenerateSceneGuidToPathMap(allSceneGuids);
                WriteSceneGuidToPathMap(sceneGuidToPathMap);
            }
            finally
            {
                AssetDatabase.Refresh();
            }
#endif
        }

#if UNITY_EDITOR
        internal static void EnsureScaffolding()
        {
            if (!File.Exists(SceneGuidToPathMapProvider.SavedFilePath))
            {
                File.WriteAllText(SceneGuidToPathMapProvider.SavedFilePath, "{}");
            }
        }

        private static Dictionary<string, string> GenerateSceneGuidToPathMap(string[] allSceneGuids)
        {
            var sceneGuidToPathMap = allSceneGuids.ToDictionary(
                x => x,                       // key generator: take guids
                AssetDatabase.GUIDToAssetPath // value generator: take paths
            );
            return sceneGuidToPathMap;
        }

        private static void WriteSceneGuidToPathMap(Dictionary<string, string> sceneGuidToPathMap)
        {
            string jsonRaw = JsonConvert.SerializeObject(sceneGuidToPathMap, Formatting.Indented);
            File.WriteAllText(SceneGuidToPathMapProvider.SavedFilePath, jsonRaw);

            SceneGuidToPathMapProvider.DirectAssign(sceneGuidToPathMap);
        }
#endif
    }
}