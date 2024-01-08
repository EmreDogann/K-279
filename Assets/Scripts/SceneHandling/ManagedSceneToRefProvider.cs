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
    [ScriptableObjectSingletonBase.FilePath("ManagedSceneToRefMap.generated.json", Location.ProjectSettings,
        UsageScope.EditorAndBuild)]
    public sealed class ManagedSceneToRefMapProvider : ISceneDataMapProvider<ManagedSceneToRefMapProvider>,
        ISceneDataMapGenerator
    {
        private Dictionary<string, List<ManagedSceneReference>> _managedSceneToRefMap;

        private static ManagedSceneToRefMapProvider _instance;
        internal static ManagedSceneToRefMapProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ManagedSceneToRefMapProvider();
                }

                return _instance;
            }
        }


        private static readonly ScriptableObjectSingletonBase.FilePathAttribute _filePathAttribute;
        public string SavedFilePath => _filePathAttribute != null ? _filePathAttribute.Filepath : string.Empty;
        public static string GetSavedFilePath => Instance.SavedFilePath;

        /// <summary>
        ///     The scene GUID to path map.
        /// </summary>
        public static IReadOnlyDictionary<string, List<ManagedSceneReference>> ManagedSceneToRef =>
            GetManagedSceneToRefMap();

        static ManagedSceneToRefMapProvider()
        {
            _filePathAttribute =
                ScriptableObjectSingletonBase.FilePathAttribute.Retrieve(typeof(ManagedSceneToRefMapProvider));
        }


        [Preserve]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        [InitializeInEditorMethod]
        internal static void Initialize()
        {
            LoadIfNotAlready();
        }

        private static IReadOnlyDictionary<string, List<ManagedSceneReference>> GetManagedSceneToRefMap()
        {
            return Instance._managedSceneToRefMap;
        }

        void ISceneDataMapProvider<ManagedSceneToRefMapProvider>.DirectAssign(
            Dictionary<string, object> managedSceneToRefMap)
        {
            FillWith(managedSceneToRefMap);
        }

        ISceneDataMapGenerator ISceneDataMapProvider<ManagedSceneToRefMapProvider>.GetGenerator()
        {
            return this;
        }

        private static void LoadIfNotAlready()
        {
            if (Instance._managedSceneToRefMap == null)
            {
                Load();
            }
        }

        private static void Load()
        {
#if UNITY_EDITOR
            if (!File.Exists(Instance.SavedFilePath))
            {
                Debug.LogWarning("Managed scene to ref map file not found!");
                SceneDataMapsGenerator.Run<ManagedSceneToRefMapProvider>();
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

                var loadedData =
                    JsonConvert.DeserializeObject<Dictionary<string, List<ManagedSceneReference>>>(dataToLoad);
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
                Debug.LogError("Managed scene to ref map file not found!");

                return;
            }

            var deserialized =
                JsonConvert.DeserializeObject<Dictionary<string, List<ManagedSceneReference>>>(genFile.text);
            FillWith(deserialized);
#endif
        }

        private static void FillWith(Dictionary<string, object> managedSceneToRefMap)
        {
            Instance._managedSceneToRefMap =
                managedSceneToRefMap.ToDictionary(x => x.Key, x => x.Value as List<ManagedSceneReference>);
        }

        private static void FillWith(Dictionary<string, List<ManagedSceneReference>> managedSceneToRefMap)
        {
            Instance._managedSceneToRefMap = managedSceneToRefMap;
        }

        public Dictionary<string, object> GenerateDataMap()
        {
            var output = new Dictionary<string, object>();
            foreach (ManagedScene managedScene in SceneManagerSettings.Instance.managedScenes)
            {
                var dependants = SceneDependencyResolver.GetDependants(managedScene);

                foreach (var entry in dependants)
                {
                    if (!output.ContainsKey(entry.Key))
                    {
                        output[entry.Key] = new List<ManagedSceneReference>();
                    }

                    foreach (string value in entry.Value)
                    {
                        ((List<ManagedSceneReference>)output[entry.Key]).Add(new ManagedSceneReference(
                            AssetRefType.Scene,
                            managedScene, entry.Key,
                            value));
                    }
                }
            }

            return output;
        }
    }

#if UNITY_EDITOR
    internal class ManagedSceneToRefBuildPreProcessor : IPreprocessBuildWithReport, IPostprocessBuildWithReport
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

            File.Copy(ManagedSceneToRefMapProvider.Instance.SavedFilePath,
                $"{ResourcesFolder}/{Path.GetFileName(ManagedSceneToRefMapProvider.Instance.SavedFilePath)}");
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