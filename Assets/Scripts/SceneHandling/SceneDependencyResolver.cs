using SceneHandling.Utility;
using Utils.Extensions;
using Debug = UnityEngine.Debug;

#if UNITY_EDITOR
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VYaml.Parser;
using UnityEditor;
#endif

namespace SceneHandling
{
    internal static class SceneDependencyResolver
    {
        internal static Dictionary<string, List<string>> GetDependants(ManagedScene managedScene,
            string nameFilter = "",
            string searchFolder = "")
        {
            if (!managedScene || !managedScene.SceneAsset)
            {
                Debug.LogWarning("Cannot find references because selected object is null!");
                return null;
            }

            string[] dependencyGuids = FindPossibleDependants(nameFilter, searchFolder);
            var dependencyAssets = ResolveDependants(managedScene, dependencyGuids);

            return dependencyAssets;
        }

        // internal static Task<List<string>> GetDependantsAsync(ManagedScene managedScene, string nameFilter = "",
        //     string searchFolder = "")
        // {
        //     if (!managedScene || !managedScene.SceneAsset)
        //     {
        //         Debug.LogWarning("Cannot find references because selected object is null!");
        //         return null;
        //     }
        //
        //     // Stopwatch timer = Stopwatch.StartNew();
        //     string[] dependencyGuids = FindPossibleDependants(nameFilter, searchFolder);
        //     // timer.Stop();
        //     // Debug.Log($"Find Assets Elapsed Time (ms): {timer.ElapsedMilliseconds}");
        //
        //     // timer.Restart();
        //     var dependencyAssets = ResolveDependantsAsync(managedScene, dependencyGuids);
        //     // timer.Stop();
        //     // Debug.Log($"Resolve Dependants Elapsed Time (ms): {timer.ElapsedMilliseconds}");
        //
        //     return dependencyAssets;
        //     // return dependencyAssets.Result.Select(AssetDatabase.LoadAssetAtPath<T>).ToList();
        // }

        private static string[] FindPossibleDependants(string nameFilter = "", string searchFolder = "")
        {
#if UNITY_EDITOR
            return AssetDatabase.FindAssets(
                nameFilter.TrimEnd() + (nameFilter.IsNullOrEmpty() ? "" : " ") + "t:SceneAsset",
                new[] { searchFolder.IsNullOrEmpty() ? "Assets" : searchFolder });
#else
            return new string[] {};
#endif
        }

        private static Dictionary<string, List<string>> ResolveDependants(ManagedScene managedScene,
            IReadOnlyCollection<string> guids,
            int maxRecursionLength = 3)
        {
#if UNITY_EDITOR
            string sceneGuid = managedScene.Guid;

            var assets = new Dictionary<string, List<string>>();

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                YamlParser yamlParser;
                try
                {
                    yamlParser =
                        new YamlParser(new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes(File.ReadAllText(assetPath))));
                    // Skip to start of file (to the tag of the first document).
                    yamlParser.SkipAfter(ParseEventType.StreamStart);
                }
                catch (Exception)
                {
                    // Debug.Log($"{assetPath}: {e}");
                    continue;
                }


                Anchor currentFileID = new Anchor("", -1);
                Anchor prevFileID = new Anchor("", -1);
                string owningGO = "";
                while (yamlParser.Read())
                {
                    if (yamlParser.CurrentEventType == ParseEventType.MappingStart)
                    {
                        // 114 = "MonoBehaviour" Class ID -> https://docs.unity3d.com/Manual/ClassIDReference.html
                        if (yamlParser.TryGetCurrentTag(out Tag tag))
                        {
                            if (!tag.Suffix.Equals("114"))
                            {
                                // Skip to the end of a document (to the tag of the next document).
                                yamlParser.SkipAfter(ParseEventType.DocumentEnd);
                                continue;
                            }

                            yamlParser.TryGetCurrentAnchor(out currentFileID);

                            // Skip document header
                            yamlParser.Read();
                            yamlParser.SkipAfter(ParseEventType.MappingStart);
                        }
                    }
                    else if (yamlParser.CurrentEventType == ParseEventType.Scalar)
                    {
                        string value = yamlParser.ReadScalarAsString(); // Get key
                        switch (value)
                        {
                            case "guid":
                            {
                                value = yamlParser.ReadScalarAsString(); // Get potential guid value
                                if (value != null && value.IsValidGuid() && value.Equals(sceneGuid) &&
                                    currentFileID.Id != prevFileID.Id)
                                {
                                    // Debug.Log($"Owning GameObject: {owningGO}");
                                    // Debug.Log($"{currentFileID.Id}, {currentFileID.Name}");
                                    // Debug.Log($"Found GUID: {value}");
                                    prevFileID = currentFileID;

                                    if (assets.ContainsKey(assetPath))
                                    {
                                        assets[assetPath].Add(owningGO);
                                    }
                                    else
                                    {
                                        var objList = new List<string>();
                                        objList.Add(owningGO);
                                        assets.Add(guid, objList);
                                    }
                                }

                                break;
                            }
                            case "m_GameObject":
                                // Get GameObject fileID
                                yamlParser.Read();
                                yamlParser.SkipAfter(ParseEventType.Scalar);
                                owningGO = yamlParser.ReadScalarAsString();
                                yamlParser.SkipAfter(ParseEventType.MappingEnd);

                                break;
                        }
                    }
                }
            }

            return assets;
#endif
        }

        // private static Task<List<string>> ResolveDependantsAsync(ManagedScene managedScene,
        //     IReadOnlyCollection<string> guids,
        //     int maxRecursionLength = 3)
        // {
        //     string sceneGuid = managedScene.Guid;
        //
        //     var assets = new ConcurrentBag<string>();
        //
        //     var assetPathsFromGuid = guids.Select(AssetDatabase.GUIDToAssetPath).ToList();
        //
        //     var task = Task.Run(() =>
        //     {
        //         Parallel.ForEach(assetPathsFromGuid, assetPath =>
        //         {
        //             StreamReader lineReader = new StreamReader(assetPath);
        //             YamlStream yaml = new YamlStream();
        //             try
        //             {
        //                 yaml.Load(lineReader);
        //             }
        //             catch (Exception e)
        //             {
        //                 Debug.Log($"{assetPath}: {e}");
        //                 return;
        //             }
        //
        //             foreach (YamlDocument doc in yaml.Documents)
        //             {
        //                 string fileID = doc.RootNode.Tag.Value[(doc.RootNode.Tag.Value.LastIndexOf(':') + 1)..];
        //                 // 114 = "MonoBehaviour" Class ID -> https://docs.unity3d.com/Manual/ClassIDReference.html
        //                 if (!fileID.Equals("114"))
        //                 {
        //                     continue;
        //                 }
        //
        //                 YamlMappingNode mapping = (YamlMappingNode)doc.RootNode;
        //
        //                 // Example: Anchor: 2126224321, Tag: MonoBehaviour
        //                 // Debug.Log($"Anchor: {mapping.Anchor}, Tag: {UnityYAMLClassID.IDToType[int.Parse(fileID)]}");
        //                 YamlMappingNode mappingWithStrippedHeader = (YamlMappingNode)mapping.Children[0].Value;
        //                 // Skip the first 5 default values for a monobehaviour -> https://docs.unity3d.com/2022.3/Documentation/Manual/YAMLSceneExample.html
        //                 // I think there are actually 10 default values we could skip, but the docs above only show 5, so better to play it on the safe side.
        //                 if (ParseChildren(mappingWithStrippedHeader.Children, sceneGuid, maxRecursionLength, 5))
        //                 {
        //                     assets.Add(assetPath);
        //                 }
        //             }
        //         });
        //
        //         return assets.ToList();
        //     });
        //
        //     return task;
        // }

        // private static bool ParseChildren(IOrderedDictionary<YamlNode, YamlNode> childrenMap, string targetGuid,
        //     int recursionLimit, int startIndex = 0)
        // {
        //     if (recursionLimit == 0)
        //     {
        //         // Debug.LogWarning("Reached Max Recursion Limit!");
        //         return false;
        //     }
        //
        //     for (int i = startIndex; i < childrenMap.Count; i++)
        //     {
        //         (YamlNode key, YamlNode value) = childrenMap[i];
        //         // Debug.Log($"{key}: {value}, {value.NodeType}");
        //         switch (value.NodeType)
        //         {
        //             case YamlNodeType.Mapping:
        //                 if (ParseChildren(((YamlMappingNode)value).Children, targetGuid, recursionLimit - 1))
        //                 {
        //                     return true;
        //                 }
        //
        //                 break;
        //             case YamlNodeType.Scalar:
        //                 if (key.ToString().Contains("guid"))
        //                 {
        //                     string guidValue = value.ToString();
        //                     if (guidValue.IsValidGuid() && guidValue.Equals(targetGuid))
        //                     {
        //                         Debug.Log($"Found GUID! {key}: {value}");
        //                         return true;
        //                     }
        //                 }
        //
        //                 break;
        //         }
        //
        //         // Debug.Log(((YamlScalarNode)entry.Key).Value);
        //     }
        //
        //     return false;
        // }
    }
}