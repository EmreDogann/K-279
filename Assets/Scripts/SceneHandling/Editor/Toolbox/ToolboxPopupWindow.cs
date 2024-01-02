using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SceneHandling.Editor.Toolbox
{
    internal class ToolboxPopupWindow : PopupWindowContent
    {
        private readonly IReadOnlyList<ITool> _tools;
        private readonly string _title;

        public ToolboxPopupWindow(IReadOnlyList<ITool> tools, string popupTitle = "")
        {
            _tools = tools;
            _title = popupTitle;
        }

        public override void OnGUI(Rect rect)
        {
            if (!string.IsNullOrEmpty(_title))
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label(_title, EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            if (_tools.Count == 0)
            {
                GUILayout.Space(2);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("No tools available.", new GUIStyle { normal = { textColor = Color.grey } });
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            foreach (ITool tool in _tools)
            {
                tool.Draw(Close);
            }
        }

        public override Vector2 GetWindowSize()
        {
            const int width = 150;
            const int bottomPadding = 5;
            float contentHeight = _tools.Count == 0
                ? EditorGUIUtility.singleLineHeight
                : _tools.Sum(x => x.GetHeight());

            float titleHeight = string.IsNullOrEmpty(_title) ? 0.0f : EditorGUIUtility.singleLineHeight;
            return new Vector2(width, titleHeight + contentHeight + bottomPadding);
        }

        private void Close()
        {
            editorWindow.Close();
        }
    }
}