using System;
using Utils;
using Utils.ObservableCollections;
using Location = SceneHandling.FilePathAttribute.Location;
using UsageScope = SceneHandling.FilePathAttribute.UsageScope;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SceneHandling
{
    [FilePath(typeof(SceneManagerSettings), Location.ProjectSettings, UsageScope.EditorAndBuild)]
    internal class SceneManagerSettings : ScriptableObjectSingleton<SceneManagerSettings>
    {
#if UNITY_EDITOR
        /// <summary>
        ///     All the scenes in the project.
        /// </summary>
        /// <remarks>Updated on every OnPostprocessAllAssets() trigger.</remarks>
        public ObservableList<SceneAsset> scenes = new ObservableList<SceneAsset>();
#endif

        /// <summary>
        ///     All scenes that are managed/referenced by the <see cref="SceneManager" />, or referenced in scene via a <see cref="ManagedScene" /> field.
        /// </summary>
        public ObservableList<ManagedScene> managedScenes = new ObservableList<ManagedScene>();

        public string settingsRootPath = "Assets/Settings/Scene Manager";

        public static void Initialize(Action callback)
        {
#if UNITY_EDITOR
            EditorApplication.delayCall += InitializeInternal;
            StaticCoroutine.RunCallback(callback,
                () => _isInitialized && !EditorApplication.isUpdating && !EditorApplication.isCompiling);
#else
            InitializeInternal();
            StaticCoroutine.RunCallback(callback, () => IsInitialized);
#endif
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            _isInitialized = true;
        }

        private static bool _isInitialized;

        private static void InitializeInternal()
        {
            SceneGuidToPathMapProvider.Initialize();
            ManagedSceneToRefMapProvider.Initialize();

            // This does a dummy get of the instance, which will trigger the instance to (lazy) load/create itself if it wasn't loaded already.
            _ = Instance;
        }

        public void ResetState()
        {
            scenes.Clear();
            SceneManagerAssets.DeleteAssets(managedScenes);
        }
    }
}