using UnityEngine;

namespace Lights
{
    public class LightFollower : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;
        [SerializeField] private float followSpeed;

        private Vector3 velocity;

        private void Update()
        {
            transform.position = Vector3.SmoothDamp(transform.position, followTarget.position, ref velocity,
                1 / followSpeed, Mathf.Infinity);
        }
    }
}