using Cinemachine;
using MyBox;
using ScriptableObjects;
using Surface;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private LayerMask collisionDetectionLayerMask;
    [SerializeField] private float collisionRadius;
    [SerializeField] private bool play3D;

    [Separator("Camera")]
    public bool shakeCameraOnStep;
    [ConditionalField(nameof(shakeCameraOnStep))]
    [SerializeField] private CinemachineIndependentImpulseListener impulseListener;
    [ConditionalField(nameof(shakeCameraOnStep))] [SerializeField] private CinemachineImpulseSource impulseSource;
    [ConditionalField(nameof(shakeCameraOnStep))] [SerializeField] private ScreenShakeProfile screenShakeProfile;

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

                if (shakeCameraOnStep && screenShakeProfile && impulseSource)
                {
                    impulseSource.m_ImpulseDefinition.m_ImpulseDuration = screenShakeProfile.impactTime;
                    impulseSource.m_DefaultVelocity = screenShakeProfile.defaultVelocity;
                    impulseListener.m_ReactionSettings.m_AmplitudeGain = screenShakeProfile.listenerAmplitude;
                    impulseListener.m_ReactionSettings.m_FrequencyGain = screenShakeProfile.listenerFrequency;
                    impulseListener.m_ReactionSettings.m_Duration = screenShakeProfile.listenerDuration;

                    impulseSource.GenerateImpulseAtPositionWithVelocity(transform.position,
                        screenShakeProfile.defaultVelocity * screenShakeProfile.impactForce);
                }
            }
        }
    }
}