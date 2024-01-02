using System.IO;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace SceneHandling
{
    /// <summary>
    ///     <para>Generic class for storing Editor state.</para>
    /// </summary>
    public class ScriptableObjectSingleton<T> : ScriptableObjectSingletonBase where T : ScriptableObjectSingleton<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                // Singleton is loaded using PlayerSettings.GetPreloadedAssets(), meaning that it is loaded right at
                // the start of the application and is only unloaded at application quit.
                // When loaded at app startup, OnEnable() is called, which is where we can set the instance.
#if UNITY_EDITOR
                if (_instance == null)
                {
                    Debug.LogWarning(
                        $"Could not find singleton instance of {typeof(T)}, attempting to create or load one...");
                    Instance = ScriptableObjectSingletonUtil.CreateOrLoad(typeof(T)) as T;

                    if (_instance == null)
                    {
                        Debug.LogError("Could not Create or Load SceneManagerSettings!");
                        return null;
                    }
                }
#endif

                return _instance;
            }
            private set
            {
                // TODO - Possible Memory Leak here
                _instance = value;

#if UNITY_EDITOR
                if (_instance)
                {
                    FilePathAttribute attribute = FilePathAttribute.Retrieve(typeof(T));

                    if (attribute != null)
                    {
                        _instance.ShouldPersist = true;
                        _instance.IncludeInBuild = attribute.Scope == FilePathAttribute.UsageScope.EditorAndBuild;
                    }
                }
#endif
            }
        }

#if UNITY_EDITOR

        private SerializedObject _serializedObject;

        /// <summary>Gets a cached <see cref="UnityEditor.SerializedObject" /> for this <see cref="ScriptableObjectSingleton{T}" />.</summary>
        /// <remarks>Only available in editor.</remarks>
        public SerializedObject SerializedObject => _serializedObject ??= new SerializedObject(this);

#endif

        protected ScriptableObjectSingleton()
        {
            if (_instance != null)
            {
                Debug.LogError(
                    "ScriptableSingleton already exists. Did you query the singleton in a constructor?");
            }
            else
            {
                _instance = (object)this as T;
            }
        }


        protected override void OnEnable()
        {
            base.OnEnable();

            // Reached at application start during build due to use of PlayerSettings.GetPreloadedAssets().
            _instance = (object)this as T;

            OnLoad();
        }

        /// <summary>
        ///     Tries to get the relative file path this singleton is persisted to (relative to project folder).
        /// </summary>
        /// <remarks>If singleton is not configured as persistent, then returns sting.Empty.</remarks>
        /// <param name="absolutePath">Should we return an absolute path (True) or a relative to project folder path (False, default).</param>
        /// <returns>File path on disk to where singleton is persisted.</returns>
        public string TryGetFilePath(bool absolutePath = false)
        {
            if (!_instance.ShouldPersist)
            {
                Debug.LogWarning(
                    $"Cannot get file path of ScriptableObjectSingleton {typeof(T)} because it is not configured to be persistent.");
                return string.Empty;
            }

            FilePathAttribute attribute = FilePathAttribute.Retrieve(typeof(T));
            if (absolutePath)
            {
                return Path.GetFullPath(attribute.Filepath);
            }

            return attribute.Filepath;
        }

        /// <summary>
        ///     Tries to get the directory this singleton is persisted to.
        /// </summary>
        /// <remarks>If singleton is not configured as persistent, then returns sting.Empty.</remarks>
        /// <param name="absolutePath">Should we return an absolute path (True) or a relative to project folder path (False, default).</param>
        /// <returns>Directory on disk to where singleton is persisted.</returns>
        public string TryGetFilePathDirectory(bool absolutePath = false)
        {
            if (!_instance.ShouldPersist)
            {
                Debug.LogWarning(
                    $"Cannot get file path of ScriptableObjectSingleton {typeof(T)} because it is not configured to be persistent.");
                return string.Empty;
            }

            FilePathAttribute attribute = FilePathAttribute.Retrieve(typeof(T));
            if (absolutePath)
            {
                return Path.GetDirectoryName(Path.GetFullPath(attribute.Filepath));
            }

            return Path.GetDirectoryName(attribute.Filepath);
        }

        /// <summary>
        ///     Saves singleton to disk.
        /// </summary>
        /// <remarks>Not available in build.</remarks>
        public void Save()
        {
#if UNITY_EDITOR
            if (EditorApplication.isUpdating)
            {
                EditorApplication.delayCall += Save;
            }
            else
            {
                ScriptableObjectSingletonUtil.Save(_instance);
            }
#endif
        }

        /// <summary>
        ///     Loads singleton asset from disk.
        /// </summary>
        /// <remarks>Not available in build.</remarks>
        public void Load()
        {
#if UNITY_EDITOR
            if (EditorApplication.isUpdating)
            {
                EditorApplication.delayCall += Load;
            }
            else
            {
                Destroy();
                Instance = ScriptableObjectSingletonUtil.Load(typeof(T)) as T;
            }
#endif
        }

        /// <summary>
        ///     Deletes the existing singleton asset from disk and creates a new one in its place.
        /// </summary>
        public void Create()
        {
#if UNITY_EDITOR
            if (EditorApplication.isUpdating)
            {
                EditorApplication.delayCall += Create;
            }
            else
            {
                Destroy();
                Instance = ScriptableObjectSingletonUtil.Create(typeof(T)) as T;
            }
#endif
        }

//         /// <summary>
//         ///     Deletes the existing singleton asset from disk. Does not work in build.
//         /// </summary>
//         public void Delete()
//         {
// #if UNITY_EDITOR
//             if (EditorApplication.isUpdating)
//             {
//                 EditorApplication.delayCall += Delete;
//             }
//             else
//             {
//                 Destroy();
//             }
// #endif
//         }


        private static void Destroy()
        {
            DestroyImmediate(_instance);
            Instance = null;
        }
    }
}