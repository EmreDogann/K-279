using Cinemachine;
using UnityEngine;

namespace Rooms
{
    [RequireComponent(typeof(CinemachineConfiner2D))]
    public class CameraConfiner2DSwitcher : MonoBehaviour
    {
        private CinemachineConfiner2D _confiner2D;

        private void Awake()
        {
            _confiner2D = GetComponent<CinemachineConfiner2D>();
        }

        public void OnApplicationQuit()
        {
            _confiner2D.m_BoundingShape2D = new Collider2D();
            _confiner2D.InvalidateCache();
        }

        public void SwitchConfinerTarget(Collider2D collider)
        {
            _confiner2D.m_BoundingShape2D = collider;
            _confiner2D.InvalidateCache();
        }
    }
}