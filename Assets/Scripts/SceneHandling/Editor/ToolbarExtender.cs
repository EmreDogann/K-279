using UnityEditor;
using UnityEngine;

namespace SceneHandling.Editor
{
    internal static class ToolbarStyles
    {
        public static readonly GUIStyle CommandButtonStyle;

        static ToolbarStyles()
        {
            CommandButtonStyle = new GUIStyle("AppCommandMid")
            {
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold,
                fixedHeight = 20
            };
        }
    }

    [InitializeOnLoad]
    internal static class ToolbarExtender
    {
        static ToolbarExtender()
        {
            SceneManager.OnInitialized += OnInitialized;
        }

        private static void OnInitialized()
        {
            UnityToolbarExtender.ToolbarExtender.LeftToolbarGUI.Add(DrawToolbar);
            SceneManager.OnInitialized -= OnInitialized;
        }

        private static void DrawToolbar()
        {
            bool isOrWillEnterPlaymode = EditorApplication.isPlayingOrWillChangePlaymode;
            GUILayout.FlexibleSpace();

            GUIContent content = isOrWillEnterPlaymode
                ? EditorGUIUtility.IconContent("PlayButton On", "Stop Play")
                : EditorGUIUtility.TrIconContent("PlayButton", "Play with Scene Preset");

            if (GUILayout.Toggle(isOrWillEnterPlaymode, content, ToolbarStyles.CommandButtonStyle))
            {
                // TODO: Enter Play mode using Scene Presets...
            }
        }
    }
}