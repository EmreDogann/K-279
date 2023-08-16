using MyBox;
using UnityEngine;

namespace Cinemachine
{
    /// <summary>
    ///     An add-on module for Cinemachine Virtual Camera that locks the camera's Z co-ordinate
    /// </summary>
    [ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
    public class LockCameraZ : CinemachineExtension
    {
        [SerializeField] private CinemachineCore.Stage desiredStage;

        [SerializeField] private bool useFramingTransposerPosition;
        [ConditionalField(nameof(useFramingTransposerPosition))] [SerializeField] private bool invertZ = true;

        [Space]
        [Tooltip("Lock the camera's Z position to this value")]
        [ConditionalField(nameof(useFramingTransposerPosition), true)] [SerializeField] private float zPosition = 10;

        private CinemachineFramingTransposer _framingTransposer;

        private void OnValidate()
        {
            _framingTransposer = ((CinemachineVirtualCamera)VirtualCamera)
                .GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == desiredStage)
            {
                if (!useFramingTransposerPosition)
                {
                    Vector3 pos = state.RawPosition;
                    pos.z = zPosition;
                    state.RawPosition = pos;
                }
                else if (useFramingTransposerPosition && _framingTransposer != null)
                {
                    Vector3 pos = state.RawPosition;
                    pos.z = _framingTransposer.m_CameraDistance * (invertZ ? -1.0f : 1.0f);
                    state.RawPosition = pos;
                }
            }
        }
    }
}