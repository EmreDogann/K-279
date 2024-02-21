using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
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
            var guidToAssetPath = dependencyGuids
                .Select(e => new KeyValuePair<string, string>(e, AssetDatabase.GUIDToAssetPath(e)))
                .ToList();

            var dependencyAssets = ResolveDependant(managedScene, guidToAssetPath);
            return dependencyAssets;
        }

        internal static Task<Dictionary<string, List<string>>> GetDependantsAsync(ManagedScene managedScene,
            string nameFilter = "",
            string searchFolder = "")
        {
            if (!managedScene || !managedScene.SceneAsset)
            {
                Debug.LogWarning("Cannot find references because selected object is null!");
                return null;
            }

            // Stopwatch timer = Stopwatch.StartNew();
            string[] dependencyGuids = FindPossibleDependants(nameFilter, searchFolder);
            // timer.Stop();
            // Debug.Log($"Find Assets Elapsed Time (ms): {timer.ElapsedMilliseconds}");

            // timer.Restart();

            var dependencyAssets = ResolveDependantAsync(managedScene,
                dependencyGuids.Select(e => new KeyValuePair<string, string>(e, AssetDatabase.GUIDToAssetPath(e)))
                    .ToList());
            // timer.Stop();
            // Debug.Log($"Resolve Dependants Elapsed Time (ms): {timer.ElapsedMilliseconds}");

            return dependencyAssets;
            // return dependencyAssets.Result.Select(AssetDatabase.LoadAssetAtPath<T>).ToList();
        }

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

        // assetPaths: Key - GUID, Value: Asset Path
        private static Dictionary<string, List<string>> ResolveDependant(ManagedScene managedScene,
            IReadOnlyCollection<KeyValuePair<string, string>> assetPaths,
            int maxRecursionLength = 3)
        {
#if UNITY_EDITOR
            string sceneGuid = managedScene.Guid;
            var assets = new Dictionary<string, List<string>>();

            foreach (var assetPath in assetPaths)
            {
                YamlParser yamlParser;
                try
                {
                    yamlParser =
                        new YamlParser(
                            new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes(File.ReadAllText(assetPath.Value))));
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

                                    if (assets.ContainsKey(assetPath.Key))
                                    {
                                        assets[assetPath.Key].Add(owningGO);
                                    }
                                    else
                                    {
                                        var objList = new List<string> { owningGO };
                                        assets.Add(assetPath.Key, objList);
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

        private static Task<Dictionary<string, List<string>>> ResolveDependantAsync(ManagedScene managedScene,
            IReadOnlyCollection<KeyValuePair<string, string>> guidToAssetPath,
            int maxRecursionLength = 3)
        {
            var assets = new ConcurrentDictionary<string, ConcurrentBag<string>>();

            var task = Task.Run(() =>
            {
                Parallel.ForEach(guidToAssetPath, entry =>
                {
                    var dependants = ResolveDependant(managedScene, new[] { entry }, maxRecursionLength);
                    foreach (var dependant in dependants)
                    {
                        if (assets.TryGetValue(dependant.Key, out var asset))
                        {
                            foreach (string value in dependant.Value)
                            {
                                if (!asset.Contains(value))
                                {
                                    asset.Add(value);
                                }
                            }

                            assets.TryGetValue(dependant.Key, out var existing);
                            assets.TryUpdate(dependant.Key, asset, existing);
                        }
                        else
                        {
                            assets.TryAdd(dependant.Key, new ConcurrentBag<string>(dependant.Value));
                        }
                    }
                });

                return assets.ToDictionary(entry => entry.Key, entry => entry.Value.ToList());
            });

            return task;
        }
    }
}