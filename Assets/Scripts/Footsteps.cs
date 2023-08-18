using Surface;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private LayerMask collisionDetectionLayerMask;
    [SerializeField] private float collisionRadius;

    private RaycastHit _hit;

    private void TriggerFootstep()
    {
        if (Physics.SphereCast(transform.position + Vector3.up * (collisionRadius + 0.5f), collisionRadius,
                Vector3.down, out _hit,
                collisionRadius,
                collisionDetectionLayerMask))
        {
            if (_hit.transform.TryGetComponent(out ISteppable component))
            {
                component.GetSurfaceData().surfaceSound.Play2D();
            }
        }
    }
}