using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace RenderFeatures
{
    public class ScreenDitherRenderPass : ScriptableRenderPass, IDisposable
    {
        // Settings
        private readonly string _profilerTag;

        // Resources
        private readonly Material _ditherMaterial;
        private readonly Material _thresholdMaterial;
        private readonly Texture2D _blueNoiseTexture;
        private readonly Texture2D _rampTexture;
        private readonly float _threshold;
        private readonly float _tiling;
        private readonly float _sharpenIntensity;
        private readonly bool _useScrolling;
        private readonly FilterMode _filterMode;

        // Render Targets
        private RenderTargetIdentifier _colorTarget;
        private readonly RenderTargetIdentifier _superTarget;
        private readonly RenderTargetIdentifier _halfTarget;
        private RenderTextureDescriptor _cameraDescriptor, _targetsDescriptor;

        // Shader property IDs
        private readonly int NoiseTexProperty = Shader.PropertyToID("_NoiseTex");
        private readonly int ColorRampTexProperty = Shader.PropertyToID("_ColorRampTex");
        private readonly int XOffsetProperty = Shader.PropertyToID("_XOffset");
        private readonly int YOffsetProperty = Shader.PropertyToID("_YOffset");
        private readonly int ThresholdProperty = Shader.PropertyToID("_Threshold");
        private readonly int TilingProperty = Shader.PropertyToID("_Tiling");

        private readonly int SuperID = Shader.PropertyToID("Super");
        private readonly int HalfID = Shader.PropertyToID("Half");
        private static readonly int BL = Shader.PropertyToID("_BL");
        private static readonly int TL = Shader.PropertyToID("_TL");
        private static readonly int TR = Shader.PropertyToID("_TR");
        private static readonly int BR = Shader.PropertyToID("_BR");
        private static readonly int Intensity = Shader.PropertyToID("_Intensity");

        // The constructor of the pass. Here you can set any material properties that do not need to be updated on a per-frame basis.
        public ScreenDitherRenderPass(string profilerTag, RenderPassEvent renderEvent, Texture2D ditherTex,
            Texture2D rampTex, float threshold, float tiling, float sharpenIntensity, bool useScrolling,
            FilterMode filterMode)
        {
            _profilerTag = profilerTag;
            renderPassEvent = renderEvent;

            _threshold = threshold;
            _tiling = tiling;
            _sharpenIntensity = sharpenIntensity;
            _useScrolling = useScrolling;
            _filterMode = filterMode;

            if (_ditherMaterial == null)
            {
                _ditherMaterial = CoreUtils.CreateEngineMaterial("Hidden/Dither/ScreenSpaceDither");
                _blueNoiseTexture = ditherTex;
                _rampTexture = rampTex;

                if (_blueNoiseTexture != null)
                {
                    _ditherMaterial.SetTexture(NoiseTexProperty, _blueNoiseTexture);
                }

                _ditherMaterial.SetTexture(ColorRampTexProperty, _rampTexture);
                _ditherMaterial.SetFloat(ThresholdProperty, threshold);
                _ditherMaterial.SetFloat(TilingProperty, tiling);
            }

            if (_thresholdMaterial == null)
            {
                _thresholdMaterial = CoreUtils.CreateEngineMaterial("Hidden/Dither/Threshold");
            }

            _superTarget = new RenderTargetIdentifier(SuperID);
            _halfTarget = new RenderTargetIdentifier(HalfID);
        }

        // Called per-pass
        // Same as OnCameraSetup() below.
        // public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        // {
        // ConfigureTarget(_mipUpTarget);
        // ConfigureClear(ClearFlag.All, Color.black);
        // }

        // Called per-camera.
        // Gets called by the renderer before executing the pass.
        // Can be used to configure render targets and their clearing state.
        // Can be used to create temporary render target textures.
        // If this method is not overriden, the render pass will render to the active camera render target.
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            // Grab the camera target descriptor. We will use this when creating a temporary render texture.
            _cameraDescriptor = renderingData.cameraData.cameraTargetDescriptor;

            _targetsDescriptor = _cameraDescriptor;
            _targetsDescriptor.depthBufferBits = 0;

            // _targetsDescriptor.width <<= 1;
            // _targetsDescriptor.height <<= 1;
            cmd.GetTemporaryRT(SuperID, _targetsDescriptor, _filterMode);

            // _targetsDescriptor.width >>= 2;
            // _targetsDescriptor.height >>= 2;
            cmd.GetTemporaryRT(HalfID, _targetsDescriptor, _filterMode);

            float xOffset = 0.0f;
            float yOffset = 0.0f;

            if (_useScrolling)
            {
                Vector3 camEuler = renderingData.cameraData.camera.transform.eulerAngles;
                xOffset = 4.0f * camEuler.y / renderingData.cameraData.camera.fieldOfView;
                yOffset = -2.0f * renderingData.cameraData.camera.aspect * camEuler.x /
                          renderingData.cameraData.camera.fieldOfView;
            }

            _ditherMaterial.SetFloat(XOffsetProperty, xOffset);
            _ditherMaterial.SetFloat(YOffsetProperty, yOffset);

            var corners = new Vector3[4];

            renderingData.cameraData.camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1),
                renderingData.cameraData.camera.farClipPlane,
                Camera.MonoOrStereoscopicEye.Mono, corners);

            for (int i = 0; i < 4; i++)
            {
                corners[i] = renderingData.cameraData.camera.transform.TransformVector(corners[i]);
                corners[i].Normalize();
            }

            _ditherMaterial.SetVector(BL, corners[0]);
            _ditherMaterial.SetVector(TL, corners[1]);
            _ditherMaterial.SetVector(TR, corners[2]);
            _ditherMaterial.SetVector(BR, corners[3]);

            _ditherMaterial.SetFloat(ThresholdProperty, _threshold);
            _ditherMaterial.SetFloat(TilingProperty, _tiling);
        }

        public void SetTarget(RenderTargetIdentifier camerColorTarget)
        {
            _colorTarget = camerColorTarget;
        }

        // The actual execution of the pass. This is where custom rendering occurs.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, new ProfilingSampler(_profilerTag)))
            {
                Blit(cmd, _colorTarget, _superTarget, _ditherMaterial);
                Blit(cmd, _superTarget, _halfTarget, _thresholdMaterial);
                Blit(cmd, _halfTarget, _colorTarget);
            }

            // Execute the command buffer and release it.
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();

            CommandBufferPool.Release(cmd);
        }

        // Called when the camera has finished rendering.
        // Here we release/cleanup any allocated resources that were created by this pass.
        // Gets called for all cameras in a camera stack.
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            // Since we created a temporary render texture in OnCameraSetup, we need to release the memory here to avoid a leak.
            cmd.ReleaseTemporaryRT(SuperID);
            cmd.ReleaseTemporaryRT(HalfID);
        }

        public void Dispose()
        {
            CoreUtils.Destroy(_ditherMaterial);
            CoreUtils.Destroy(_thresholdMaterial);
        }
    }
}