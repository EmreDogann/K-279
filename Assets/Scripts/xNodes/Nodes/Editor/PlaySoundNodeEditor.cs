using UnityEditor;
using XNode;
using XNodeEditor;

namespace xNodes.Nodes.Editor
{
    [CustomNodeEditor(typeof(PlaySoundNode))]
    public class PlaySoundNodeEditor : NodeEditor
    {
        private PlaySoundNode _playSoundNode;

        private SerializedProperty _audioProperty;
        private SerializedProperty _playModeProperty;
        private SerializedProperty _transformProperty;

        public override void OnCreate()
        {
            _audioProperty = serializedObject.FindProperty("audio");
            _playModeProperty = serializedObject.FindProperty("playMode");
            _transformProperty = serializedObject.FindProperty("transform");
        }

        public override void OnBodyGUI()
        {
            if (_playSoundNode == null)
            {
                _playSoundNode = target as PlaySoundNode;
            }

            // Update serialized object's representation
            serializedObject.Update();

            foreach (NodePort port in target.Ports)
            {
                NodeEditorGUILayout.PortField(port);
            }

            NodeEditorGUILayout.PropertyField(_playModeProperty);
            NodeEditorGUILayout.PropertyField(_audioProperty);

            switch ((PlaySoundNode.PlayMode)_playModeProperty.enumValueIndex)
            {
                case PlaySoundNode.PlayMode.ThreeD:
                    NodeEditorGUILayout.PropertyField(_transformProperty);

                    break;
                case PlaySoundNode.PlayMode.Attached:
                    NodeEditorGUILayout.PropertyField(_transformProperty);

                    break;
            }

            // Apply property modifications
            serializedObject.ApplyModifiedProperties();
        }
    }
}