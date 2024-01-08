using System;
using System.Runtime.Serialization;
using SceneHandling.Utility;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SceneHandling
{
    public enum AssetRefType
    {
        Scene,
        Prefab,
        ScriptableObject
    }

    public class ManagedSceneReference
    {
        [NonSerialized] private Object sceneAsset;
        public string AssetRefGuid { get; set; }
        public AssetRefType RefType { get; set; }
        public string ObjectRefID { get; set; }
        public ManagedScene ManagedScene { get; set; }

#if UNITY_EDITOR
        /// <summary>
        ///     Get the <see cref="SceneAsset" /> that is managed by this instance.
        /// </summary>
        /// <remarks>Only available in editor.</remarks>
        [IgnoreDataMember]
        public SceneAsset SceneAsset
        {
            get => sceneAsset as SceneAsset;
            internal set => sceneAsset = value;
        }
#endif

        // Parameter names in constructor must match the serialized names otherwise Newtonsoft.Json won't be able to find and populate them with the appropriate data.
        public ManagedSceneReference(AssetRefType refType, ManagedScene managedScene, string assetRefGuid,
            string objectRefID)
        {
            RefType = refType;
            ManagedScene = managedScene;
            AssetRefGuid = assetRefGuid;
            ObjectRefID = objectRefID;

            if (!SceneGuidToPathMapProvider.GuidToPathMap.TryGetValue(assetRefGuid, out string pathFromMap))
            {
                throw new Exception(
                    $"Given GUID is not found in the scene GUID to path map. GUID: '{assetRefGuid}'"
                    + "\nThis can happen for these reasons:"
                    + "\n1. The asset with the given GUID either doesn't exist or is not a scene. To fix this, make sure you provide the GUID of a valid scene."
                    + "\n2. The scene GUID to path map is outdated.");
            }

#if UNITY_EDITOR

            Object foundAsset = AssetDatabase.LoadAssetAtPath<Object>(pathFromMap);

            if (!foundAsset)
            {
                throw new Exception(
                    $"The given GUID was found in the map, but the scene asset at the corresponding path could not be loaded. Path: '{pathFromMap}'."
                    + "\nThis can happen due to an outdated scene GUID to path map retaining scene assets that no longer exist.");
            }

            sceneAsset = foundAsset;
#endif
        }

        /// <summary>
        ///     Is this <see cref="ManagedSceneReference" /> assigned something?
        /// </summary>
        [IgnoreDataMember]
        public bool HasValue
        {
            get
            {
                if (!AssetRefGuid.IsValidGuid())
                {
                    // internal exceptions should not be documented as part of the public API
                    throw new Exception($"GUID is invalid. GUID: {AssetRefGuid}");
                }

                return AssetRefGuid != GuidUtils.AllZeroGuid;
            }
        }

        /// <summary>
        ///     Path to the referenced asset
        /// </summary>
        [IgnoreDataMember]
        public string AssetRefPath
        {
            get
            {
                if (!HasValue)
                {
                    throw new Exception($"This {typeof(ManagedSceneReference).Name} is not assigned a reference.");
                }

                switch (RefType)
                {
                    case AssetRefType.Scene:
                        if (!SceneGuidToPathMapProvider.GuidToPathMap.TryGetValue(AssetRefGuid, out string pathFromMap))
                        {
                            throw new Exception(
                                $"Given GUID is not found in the scene GUID to path map. GUID: '{AssetRefGuid}'"
                                + "\nThis can happen for these reasons:"
                                + "\n1. The asset with the given GUID either doesn't exist or is not a scene. To fix this, make sure you provide the GUID of a valid scene."
                                + "\n2. The scene GUID to path map is outdated.");
                        }

                        return pathFromMap;
                    case AssetRefType.Prefab:
                        break;
                    case AssetRefType.ScriptableObject:
                        break;
                }

                return "";
            }
        }
    }
}