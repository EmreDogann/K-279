using System;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace SceneHandling.Editor.MapGeneratorTriggers
{
    internal class MapTriggerBuildPreProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => -100;

        public void OnPreprocessBuild(BuildReport report)
        {
            try
            {
                SceneDataMapsGenerator.Run<SceneGuidToPathMapProvider>();
                SceneDataMapsGenerator.Run<ManagedSceneToRefMapProvider>();
            }
            catch (Exception ex)
            {
                Debug.LogError("Failing the build due to failure during scene data maps generation.");

                // Only a BuildFailedException fails the build, so wrapping the original exception.
                throw new BuildFailedException(ex);
            }
        }
    }
}