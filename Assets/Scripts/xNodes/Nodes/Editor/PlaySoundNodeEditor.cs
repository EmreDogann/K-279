using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;
using xNodes.Nodes.Sound;

namespace xNodes.Nodes.Editor
{
    [CustomNodeEditor(typeof(PlaySoundNode))]
    public class PlaySoundNodeEditor : BaseNodeEditor
    {
        private SerializedProperty _audioDataListProperty;
        private SerializedProperty _playModeProperty;

        private int _selectedIndex;
        private string[] _enumNamesToDisplay;

        private const string PlayModeLabel = "Global Play Mode";

        public override void OnCreate()
        {
            _audioDataListProperty = serializedObject.FindProperty("audioDataList");
            _playModeProperty = serializedObject.FindProperty("playModeGlobal");

            string[] allNames = Enum.GetNames(typeof(PlaySoundNode.PlayMode));
            _enumNamesToDisplay = allNames.Where(n => n != PlaySoundNode.PlayMode.GlobalOverride.ToString()).ToArray();

            for (int i = 0; i < _enumNamesToDisplay.Length; i++)
            {
                if (_enumNamesToDisplay[i] == ((PlaySoundNode.PlayMode)_playModeProperty.enumValueIndex).ToString())
                {
                    _selectedIndex = i;
                    break;
                }
            }
        }

        public override void OnBodyGUI()
        {
            // Update serialized object's representation
            serializedObject.Update();

            foreach (NodePort port in target.Ports)
            {
                NodeEditorGUILayout.PortField(port);
            }

            float initLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth =
                EditorStyles.label.CalcSize(new GUIContent(PlayModeLabel)).x + 64;
            _selectedIndex = EditorGUILayout.Popup(PlayModeLabel, _selectedIndex, _enumNamesToDisplay);
            _playModeProperty.enumValueIndex =
                (int)Enum.Parse<PlaySoundNode.PlayMode>(_enumNamesToDisplay[_selectedIndex]);
            EditorGUIUtility.labelWidth = initLabelWidth;

            EditorGUILayout.PropertyField(_audioDataListProperty);

            // Apply property modifications
            serializedObject.ApplyModifiedProperties();
        }
    }
}