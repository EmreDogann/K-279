using System.Collections.Generic;
using System.Linq;
using SceneHandling.Editor.Utility;
using UnityEditor;

namespace SceneHandling.Editor.MapGeneratorTriggers
{
    internal class SceneAssetPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths)
        {
            bool hasSceneChange = GetCreatedAssets(importedAssets)
                .Concat(deletedAssets)
                .Concat(movedAssets)
                .Concat(movedFromAssetPaths)
                .Any(EditorUtils.IsScenePath);

            if (hasSceneChange)
            {
                SceneDataMapsGenerator.Run<SceneGuidToPathMapProvider>();
            }
        }

        private static IEnumerable<string> GetCreatedAssets(string[] importedAssets)
        {
            // If we don't have a map, then we should treat all imported assets as created assets.
            var sceneGuidToPathMap = SceneGuidToPathMapProvider.GuidToPathMap;
            return sceneGuidToPathMap == null ? importedAssets : importedAssets.Except(sceneGuidToPathMap.Values);
        }
    }
}