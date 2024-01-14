using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using Location = SceneHandling.FilePathAttribute.Location;
using UsageScope = SceneHandling.FilePathAttribute.UsageScope;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif

namespace SceneHandling
{
    [FilePath("SceneGuidToPathMap.generated.json", Location.ProjectSettings, UsageScope.EditorAndBuild)]
    public sealed class SceneGuidToPathMapProvider : ISceneDataMapProvider<SceneGuidToPathMapProvider>,
        ISceneDataMapGenerator
    {
        private Dictionary<string, string> _sceneGuidToPathMap;
        private Dictionary<string, string> _scenePathToGuidMap;

        private static SceneGuidToPathMapProvider _instance;
        internal static SceneGuidToPathMapProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SceneGuidToPathMapProvider();
                }

                return _instance;
            }
        }

        private static readonly FilePathAttribute FilePathAttribute;
        public string SavedFilePath => FilePathAttribute != null ? FilePathAttribute.Filepath : string.Empty;
        public static string GetSavedFilePath => Instance.SavedFilePath;

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
            FilePathAttribute = FilePathAttribute.Retrieve(typeof(SceneGuidToPathMapProvider));
        }

        internal static void Initialize()
        {
            LoadIfNotAlready();
        }

        private static IReadOnlyDictionary<string, string> GetSceneGuidToPathMap()
        {
            return Instance._sceneGuidToPathMap;
        }

        private static IReadOnlyDictionary<string, string> GetScenePathToGuidMap()
        {
            return Instance._scenePathToGuidMap;
        }

        void ISceneDataMapProvider<SceneGuidToPathMapProvider>.DirectAssign(
            Dictionary<string, object> sceneGuidToPathMap)
        {
            FillWith(sceneGuidToPathMap);
        }

        ISceneDataMapGenerator ISceneDataMapProvider<SceneGuidToPathMapProvider>.GetGenerator()
        {
            return this;
        }

        private static void LoadIfNotAlready()
        {
            if (Instance._sceneGuidToPathMap == null)
            {
                Load();
            }
        }

        private static void Load()
        {
#if UNITY_EDITOR
            if (!File.Exists(Instance.SavedFilePath))
            {
                Debug.LogWarning("Scene GUID to path map file not found!");
                SceneDataMapsGenerator.Run<SceneGuidToPathMapProvider>();
            }

            try
            {
                string dataToLoad;

                using (FileStream stream = new FileStream(Instance.SavedFilePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                var loadedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataToLoad);
                FillWith(loadedData);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occured when trying to save data to file: {Instance.SavedFilePath}\n{e}");
            }
#elif UNITY_STANDALONE
            var genFile = Resources.Load<TextAsset>(Instance.SavedFilePath);

            if (genFile == null)
            {
                Debug.LogError("Scene GUID to path map file not found!");

                return;
            }

            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, object>>(genFile.text);
            FillWith(deserialized);
#endif
        }

        private static void FillWith(Dictionary<string, object> sceneGuidToPathMap)
        {
            Instance._sceneGuidToPathMap = sceneGuidToPathMap.ToDictionary(x => x.Key, x => x.Value.ToString());
            Instance._scenePathToGuidMap = sceneGuidToPathMap.ToDictionary(x => x.Value.ToString(), x => x.Key);
        }

        public Dictionary<string, object> GenerateDataMap()
        {
#if UNITY_EDITOR
            string[] allSceneGuids = AssetDatabase.FindAssets("t:Scene");

            var sceneGuidToPathMap = allSceneGuids.ToDictionary(
                x => x,                                         // key generator: take guids
                x => AssetDatabase.GUIDToAssetPath(x) as object // value generator: take paths
            );
            return sceneGuidToPathMap;
#else
            return new Dictionary<string, object>();
#endif
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

            File.Copy(SceneGuidToPathMapProvider.Instance.SavedFilePath,
                $"{ResourcesFolder}/{Path.GetFileName(SceneGuidToPathMapProvider.Instance.SavedFilePath)}");
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