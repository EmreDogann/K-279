using UnityEditor;
using UnityEngine;
using xNodes.Nodes.Sound;

namespace xNodes.Nodes.Editor
{
    [CustomPropertyDrawer(typeof(PlaySoundNode.PlaySoundNode_AudioData))]
    public class PlaySoundNode_AudioDataDrawer : PropertyDrawer
    {
        private SerializedProperty _audioProperty;
        private SerializedProperty _playModeProperty;
        private SerializedProperty _transformProperty;
        private SerializedProperty _playModeGlobalProperty;

        private bool _shouldInit = true;

        private const float YOffset = 2;

        public bool IsGlobalTwoDSet => (PlaySoundNode.PlayMode)_playModeGlobalProperty.enumValueIndex ==
                                       PlaySoundNode.PlayMode.TwoD;

        public void Init(SerializedProperty serializedProperty)
        {
            _playModeGlobalProperty = serializedProperty.serializedObject.FindProperty("playModeGlobal");
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_shouldInit)
            {
                Init(property);
                _shouldInit = false;
            }

            return (EditorGUIUtility.singleLineHeight + YOffset) * (IsGlobalTwoDSet ? 2 : 3);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_shouldInit)
            {
                Init(property);
                _shouldInit = false;
            }

            _playModeProperty = property.FindPropertyRelative("playMode");
            _audioProperty = property.FindPropertyRelative("audio");
            _transformProperty = property.FindPropertyRelative("transform");

            EditorGUI.BeginProperty(position, label, property);

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, _playModeProperty);
            position.y += EditorGUIUtility.singleLineHeight + YOffset;
            EditorGUI.PropertyField(position, _audioProperty);
            position.y += EditorGUIUtility.singleLineHeight + YOffset;

            switch ((PlaySoundNode.PlayMode)_playModeProperty.enumValueIndex)
            {
                case PlaySoundNode.PlayMode.GlobalOverride:
                    if (!IsGlobalTwoDSet)
                    {
                        EditorGUI.PropertyField(position, _transformProperty);
                    }

                    break;
                case PlaySoundNode.PlayMode.ThreeD:
                    EditorGUI.PropertyField(position, _transformProperty);

                    break;
                case PlaySoundNode.PlayMode.Attached:
                    EditorGUI.PropertyField(position, _transformProperty);

                    break;
            }

            EditorGUI.EndProperty();
        }
    }
}