using Surface;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private LayerMask collisionDetectionLayerMask;
    [SerializeField] private float collisionRadius;
    [SerializeField] private bool play3D;

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
                if (!play3D)
                {
                    component.GetSurfaceData().surfaceSound.Play2D();
                }
                else
                {
                    component.GetSurfaceData().surfaceSound.Play(transform.position);
                }
            }
        }
    }
}