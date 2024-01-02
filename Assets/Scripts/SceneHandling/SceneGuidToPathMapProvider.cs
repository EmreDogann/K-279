using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting;
using Location = SceneHandling.ScriptableObjectSingletonBase.FilePathAttribute.Location;
using UsageScope = SceneHandling.ScriptableObjectSingletonBase.FilePathAttribute.UsageScope;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif

namespace SceneHandling
{
    [ScriptableObjectSingletonBase.FilePath("SceneGuidToPathMap.generated.json", Location.ProjectSettings,
        UsageScope.EditorAndBuild)]
    public static class SceneGuidToPathMapProvider
    {
        private static Dictionary<string, string> _sceneGuidToPathMap;
        private static Dictionary<string, string> _scenePathToGuidMap;

        private static readonly ScriptableObjectSingletonBase.FilePathAttribute FilePathAttribute;
        public static string SavedFilePath => FilePathAttribute != null ? FilePathAttribute.Filepath : string.Empty;

        /// <summary>
        ///     The scene GUID to path map.
        /// </summary>
        public static IReadOnlyDictionary<string, string> GuidToPathMap => GetSceneGuidToPathMap();

        /// <summary>
        ///     The scene path to GUID map.
        /// </summary>
        public static IReadOnlyDictionary<string, string> PathToGuidMap => GetScenePathToGuidMap();

        static SceneGuidToPathMapProvider()
        {
            FilePathAttribute =
                ScriptableObjectSingletonBase.FilePathAttribute.Retrieve(typeof(SceneGuidToPathMapProvider));
        }

        [Preserve]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        [InitializeInEditorMethod]
        public static void Initialize()
        {
            LoadIfNotAlready();
        }

        private static IReadOnlyDictionary<string, string> GetSceneGuidToPathMap()
        {
            return _sceneGuidToPathMap;
        }

        private static IReadOnlyDictionary<string, string> GetScenePathToGuidMap()
        {
            return _scenePathToGuidMap;
        }

        internal static void DirectAssign(Dictionary<string, string> sceneGuidToPathMap)
        {
            FillWith(sceneGuidToPathMap);
        }

        private static void LoadIfNotAlready()
        {
            if (_sceneGuidToPathMap == null)
            {
                Load();
            }
        }

        private static void Load()
        {
            if (!File.Exists(SavedFilePath))
            {
                Debug.LogWarning("Scene GUID to path map file not found!");
                SceneDataMapsGenerator.Run();
            }

#if UNITY_EDITOR
            try
            {
                string dataToLoad;

                using (FileStream stream = new FileStream(SavedFilePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                var loadedData = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataToLoad);
                FillWith(loadedData);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occured when trying to save data to file: {SavedFilePath}\n{e}");
            }
#elif UNITY_STANDALONE
            var genFile = Resources.Load<TextAsset>(SavedFilePath);

            if (genFile == null)
            {
                Debug.LogError("Scene GUID to path map file not found!");

                return;
            }

            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(genFile.text);
            FillWith(deserialized);
#endif
        }

        private static void FillWith(Dictionary<string, string> sceneGuidToPathMap)
        {
            _sceneGuidToPathMap = sceneGuidToPathMap;
            _scenePathToGuidMap = sceneGuidToPathMap.ToDictionary(x => x.Value, x => x.Key);
        }
    }

#if UNITY_EDITOR
    internal class GuidToPathMapBuildPreProcessor : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        // Must run after MapTriggerBuildPreProcessor.cs
        public int callbackOrder => -99;

        private static string ResourcesFolder => $"{SceneManagerSettings.Instance.settingsRootPath}/Resources";

        // Copies the GuidToPathMap to build so it can be loaded at runtime.
        public void OnPreprocessBuild(BuildReport report)
        {
            if (!Directory.Exists(ResourcesFolder))
            {
                Directory.CreateDirectory(ResourcesFolder);
            }

            File.Copy(SceneGuidToPathMapProvider.SavedFilePath,
                $"{ResourcesFolder}/{Path.GetFileName(SceneGuidToPathMapProvider.SavedFilePath)}");
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            Directory.Delete(ResourcesFolder);
            File.Delete($"{ResourcesFolder}.meta");

            AssetDatabase.Refresh();
        }
    }
#endif
}