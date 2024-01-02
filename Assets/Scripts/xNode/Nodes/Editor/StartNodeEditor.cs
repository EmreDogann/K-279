using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace xNode.Nodes.Editor
{
    [CustomNodeEditor(typeof(StartNode))]
    public class StartNodeEditor : BaseNodeEditor
    {
        private GUIStyle headerStyle;
        private GUIStyle headerLabelStyle;

        private SerializedProperty _exitPortProperty;

        public override void OnCreate()
        {
            headerLabelStyle = new GUIStyle(NodeEditorResources.styles.nodeHeaderLabel);
            headerLabelStyle.fontSize = 20;

            headerStyle = new GUIStyle(NodeEditorResources.styles.nodeHeader);
            headerStyle.fixedHeight += 8;

            _exitPortProperty = serializedObject.FindProperty("nodeExit");
        }

        public override void OnHeaderGUI()
        {
            GUILayout.Label(target.name, headerLabelStyle);
        }

        public override void OnBodyGUI()
        {
            // Update serialized object's representation
            serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            NodeEditorGUILayout.PropertyField(_exitPortProperty, GUIContent.none);
            EditorGUILayout.Space();

            // Apply property modifications
            serializedObject.ApplyModifiedProperties();
        }

        public override GUIStyle GetHeaderStyle()
        {
            return headerStyle;
        }

        public override GUIStyle GetHeaderLabelStyle()
        {
            return headerLabelStyle;
        }
    }
}