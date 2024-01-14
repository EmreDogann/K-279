using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SceneHandling
{
    internal static class SceneManagerAssets
    {
#if UNITY_EDITOR
        private class SceneManagerAssetsTracker : AssetPostprocessor
        {
            // ReSharper disable once Unity.IncorrectMethodSignature
            private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
                string[] movedAssets,
                string[] movedFromAssetPaths)
            {
                EditorApplication.delayCall += () =>
                {
                    // Debug.Log(
                    //     $"{importedAssets.Length}, {deletedAssets.Length}, {movedAssets.Length}, {movedFromAssetPaths.Length}");

                    // Move managed scenes
                    foreach (string asset in movedAssets) {}

                    // Import/Track new scenes
                    foreach (string asset in importedAssets) {}
                };
            }
        }

        public static ManagedScene FindManagedAsset(SceneAsset asset)
        {
            return SceneManagerSettings.Instance.managedScenes.FirstOrDefault(managedScene =>
                managedScene.SceneAsset == asset);
        }

        public static ManagedScene FindManagedAsset(ManagedScene asset)
        {
            return SceneManagerSettings.Instance.managedScenes.FirstOrDefault(managedScene => managedScene == asset);
        }

        public static ManagedScene FindManagedAsset(string guid)
        {
            return SceneManagerSettings.Instance.managedScenes.FirstOrDefault(managedScene =>
                managedScene.Guid.Equals(guid));
        }

        public static ManagedScene Create(SceneAsset asset)
        {
            ManagedScene existingManagedScene = FindManagedAsset(asset);
            if (existingManagedScene)
            {
                Debug.LogWarning("Scene is already managed by scene manager!");
                return existingManagedScene;
            }

            ManagedScene managedScene = ManagedScene.CreateFromScenePath(AssetDatabase.GetAssetPath(asset));
            // Directory.CreateDirectory(Path.GetDirectoryName(managedScene.ManagedPath) ??
            //                           throw new InvalidOperationException(
            //                               $"Could not get parent directory of path {managedScene.ManagedPath}"));

            // SaveAsset(managedScene);

            SceneManagerSettings.Instance.managedScenes.Add(managedScene);
            SaveSettings();

            return managedScene;
        }

        public static ManagedScene Create(string guid)
        {
            ManagedScene existingManagedScene = FindManagedAsset(guid);
            if (existingManagedScene)
            {
                Debug.LogWarning("Scene is already managed by scene manager!");
                return existingManagedScene;
            }

            ManagedScene managedScene = new ManagedScene(guid);
            // Directory.CreateDirectory(Path.GetDirectoryName(managedScene.ManagedPath) ??
            //                           throw new InvalidOperationException(
            //                               $"Could not get parent directory of path {managedScene.ManagedPath}"));

            SaveAsset(managedScene);

            SceneManagerSettings.Instance.managedScenes.Add(managedScene);
            SaveSettings();

            return managedScene;
        }

        private static void SaveAsset<T>(T asset) where T : ManagedScene
        {
            // asset.Save();
        }

        internal static bool DeleteAssets(IList<ManagedScene> assets)
        {
            return DeleteAssets(assets.Select(asset => asset.Guid).ToArray());
        }

        internal static bool DeleteAssets(string[] guids)
        {
            foreach (string guid in guids)
            {
                bool deleteSuccessful = DeleteAsset(guid);
                if (!deleteSuccessful)
                {
                    return false;
                }
            }

            return true;
        }

        internal static bool DeleteAsset(ManagedScene asset)
        {
            if (asset)
            {
                return DeleteAsset(asset.Guid);
            }

            Debug.LogWarning($"Cannot delete {typeof(ManagedScene).Name} because the provided asset is null.");
            return false;
        }

        internal static bool DeleteAsset(string guid)
        {
            ManagedScene existingScene = FindManagedAsset(guid);
            if (!existingScene)
            {
                Debug.LogError($"Asset with guid {guid} could not be found. Delete aborting...");
                return false;
            }

            SceneManagerSettings.Instance.managedScenes.Remove(existingScene);

            if (!existingScene.HasValue)
            {
                if (!ValidateFile(existingScene.ScenePath))
                {
                    Debug.LogError(
                        $"Could not delete {typeof(ManagedScene).Name} asset at path {existingScene.ScenePath}. File or parent directory does not exist.");
                    return false;
                }

                try
                {
                    // Using AssetDatabase.DeleteAsset here will retrigger AssetModificationProcessor.OnWillDeleteAsset(), leading to an infinite loop in the current implementation!
                    // I could work around this with some sort of flag, but there is no need, this is better.
                    // File.Delete(existingScene.ScenePath);
                    // File.Delete($"{existingScene.ManagedPath}.meta");
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    return false;
                }

                Debug.Log($"Deleted asset {typeof(ManagedScene).Name} at path {existingScene.ScenePath}.");
            }

            // Need to call Refresh() because we modified the Asset tree externally to AssetDatabase (by using System.IO.File operations).
            AssetDatabase.Refresh();
            SaveSettings();

            return true;
        }

        private static bool ValidateFile(string filePath)
        {
            bool directoryValid = ValidateDirectory(Path.GetDirectoryName(filePath));
            if (!directoryValid)
            {
                return false;
            }

            return File.Exists(filePath);
        }

        private static bool ValidateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return false;
            }

            return true;
        }

        private static void SaveSettings()
        {
            SceneManagerSettings.Instance.Save();
        }
#endif
    }
}