using Cinemachine;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Rooms
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(CinemachineConfiner2D))]
    public class CameraConfiner2DSwitcher : MonoBehaviour
    {
        private CinemachineConfiner2D _confiner2D;

        private void Awake()
        {
            _confiner2D = GetComponent<CinemachineConfiner2D>();

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += Editor_RemoveConfiner;
#endif
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= Editor_RemoveConfiner;
#endif
        }

#if UNITY_EDITOR
        public void Editor_RemoveConfiner(PlayModeStateChange stateChange)
        {
            if (stateChange == PlayModeStateChange.EnteredEditMode)
            {
                _confiner2D.m_BoundingShape2D = null;
                _confiner2D.InvalidateCache();
            }
        }
#endif

        public void RemoveConfiner()
        {
            _confiner2D.m_BoundingShape2D = null;
            _confiner2D.InvalidateCache();
        }

        public void SwitchConfinerTarget(Collider2D collider)
        {
            _confiner2D.m_BoundingShape2D = collider;
            _confiner2D.InvalidateCache();
        }
    }
}