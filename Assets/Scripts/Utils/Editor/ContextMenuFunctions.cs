using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Utils.Editor
{
    public static class ContextMenuFunctions
    {
        [MenuItem("CONTEXT/MonoBehaviour/Edit this Editor class", false, 611)]
        private static void OpenEditorScript(MenuCommand mc)
        {
            Component targetComponent = mc.context as Component;
            if (targetComponent == null)
            {
                Debug.LogError($"Component not found for command: {mc}");
                return;
            }

            UnityEditor.Editor currentEditor =
                ActiveEditorTracker.sharedTracker.activeEditors.FirstOrDefault(x => x.target == targetComponent);
            if (currentEditor == null)
            {
                Debug.LogError($"Could not find editor for component: {targetComponent.name}");
                return;
            }

            MonoScript monoScript = MonoScript.FromScriptableObject(currentEditor);
            if (monoScript == null)
            {
                monoScript = MonoScript.FromMonoBehaviour(targetComponent as MonoBehaviour);
                Debug.Log(monoScript.text);
                if (monoScript == null)
                {
                    Debug.LogError(
                        $"Could not find Script for Editor: {currentEditor.target.name}, for component: {targetComponent.name}");
                    return;
                }
            }

            string scriptPath = AssetDatabase.GetAssetPath(monoScript);
            if (string.IsNullOrEmpty(scriptPath))
            {
                Debug.LogError($"Cannot find asset for script: {monoScript.GetClass()}");
                return;
            }

            if (scriptPath.EndsWithDll() || scriptPath.EndsWithExe())
            {
                Debug.LogError($"Path to Editor: {currentEditor.name} cannot be a DLL or exe.");
                return;
            }

            Object targetScript = AssetDatabase.LoadAssetAtPath(scriptPath, typeof(MonoScript)) as MonoScript;
            if (targetScript != null)
            {
                // InternalEditorUtility.OpenFileAtLineExternal(scriptPath, 1);
                AssetDatabase.OpenAsset(targetScript);
            }
        }
    }
}