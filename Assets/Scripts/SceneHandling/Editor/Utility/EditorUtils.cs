using System.IO;
using System.Linq;
using UnityEditor;

namespace SceneHandling.Editor.Utility
{
    internal static class EditorUtils
    {
        /// <summary>
        ///     Returns true if the given <paramref name="path" /> ends with the file extension ".unity".
        /// </summary>
        public static bool IsScenePath(this string path)
        {
            return Path.GetExtension(path) == ".unity";
        }

        /// <summary>
        ///     Adds the scene with the given path to build settings as enabled.
        /// </summary>
        public static void AddSceneToBuild(string scenePath)
        {
            var tempScenes = EditorBuildSettings.scenes.ToList();
            tempScenes.Add(new EditorBuildSettingsScene(scenePath, true));
            EditorBuildSettings.scenes = tempScenes.ToArray();
        }

        /// <summary>
        ///     Enables the scene with the given guid in build settings.
        /// </summary>
        public static void EnableSceneInBuild(string sceneGuid)
        {
            var tempScenes = EditorBuildSettings.scenes.ToList();
            tempScenes.Single(x => x.guid.ToString() == sceneGuid).enabled = true;
            EditorBuildSettings.scenes = tempScenes.ToArray();
        }
    }
}