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
        private Color _backgroundColor;
        private readonly Color _middleColor;
        private Color _foregroundColor;
        private readonly float _threshold;
        private readonly float _middleColorThreshold;
        private readonly float _tiling;
        private readonly bool _worldSpaceDither;
        private readonly FilterMode _filterMode;

        // Render Targets
        private RenderTargetIdentifier _colorTarget;
        private readonly RenderTargetIdentifier _superTarget;
        private readonly RenderTargetIdentifier _halfTarget;
        private RenderTextureDescriptor _cameraDescriptor, _targetsDescriptor;

        // Shader property IDs
        private readonly int _noiseTexProperty = Shader.PropertyToID("_NoiseTex");
        private readonly int _colorRampTexProperty = Shader.PropertyToID("_ColorRampTex");
        private readonly int _mgThresholdProperty = Shader.PropertyToID("_MGThreshold");
        private readonly int _thresholdProperty = Shader.PropertyToID("_Threshold");
        private readonly int _tilingProperty = Shader.PropertyToID("_Tiling");

        private readonly int _superID = Shader.PropertyToID("Super");
        private readonly int _halfID = Shader.PropertyToID("Half");
        private readonly int _blID = Shader.PropertyToID("_BL");
        private readonly int _tlID = Shader.PropertyToID("_TL");
        private readonly int _trID = Shader.PropertyToID("_TR");
        private readonly int _brID = Shader.PropertyToID("_BR");

        private readonly int _fgID = Shader.PropertyToID("_FG");
        private readonly int _bgID = Shader.PropertyToID("_BG");
        private readonly int _mgID = Shader.PropertyToID("_MG");

        // The constructor of the pass. Here you can set any material properties that do not need to be updated on a per-frame basis.
        public ScreenDitherRenderPass(string profilerTag, RenderPassEvent renderEvent,
            ScreenDitherRenderFeature.Settings settings)
        {
            _profilerTag = profilerTag;
            renderPassEvent = renderEvent;

            _middleColorThreshold = settings.middleColorThreshold;
            _threshold = settings.threshold;
            _tiling = settings.tiling;

            _backgroundColor = settings.backgroundColor;
            _middleColor = settings.middleColor;
            _foregroundColor = settings.foregroundColor;

            _worldSpaceDither = settings.worldSpaceDither;
            _filterMode = settings.filterMode;

            if (_ditherMaterial == null)
            {
                _ditherMaterial = CoreUtils.CreateEngineMaterial("Hidden/Dither/ScreenSpaceDither");
                Texture2D blueNoiseTexture = settings.ditherTex;

                if (blueNoiseTexture != null)
                {
                    _ditherMaterial.SetTexture(_noiseTexProperty, blueNoiseTexture);
                }

                _ditherMaterial.SetTexture(_colorRampTexProperty, settings.rampTex);
                _ditherMaterial.SetFloat(_mgThresholdProperty, settings.middleColorThreshold);
                _ditherMaterial.SetFloat(_thresholdProperty, settings.threshold);
                _ditherMaterial.SetFloat(_tilingProperty, settings.tiling);
                _ditherMaterial.SetColor(_bgID, settings.backgroundColor);
                _ditherMaterial.SetColor(_mgID, settings.middleColor);
                _ditherMaterial.SetColor(_fgID, settings.foregroundColor);
            }

            if (_thresholdMaterial == null)
            {
                _thresholdMaterial = CoreUtils.CreateEngineMaterial("Hidden/Dither/Threshold");
                _thresholdMaterial.SetTexture(_colorRampTexProperty, settings.rampTex);
                _thresholdMaterial.SetColor(_bgID, settings.backgroundColor);
                _thresholdMaterial.SetColor(_fgID, settings.foregroundColor);
            }

            if (_worldSpaceDither)
            {
                _ditherMaterial.EnableKeyword("ENABLE_WORLD_SPACE_DITHER");
            }
            else
            {
                _ditherMaterial.DisableKeyword("ENABLE_WORLD_SPACE_DITHER");
                _tiling = settings.tiling + 650; // magic number to match screen-space and world-space dither.
            }

            if (settings.useRampTexture)
            {
                _ditherMaterial.EnableKeyword("USE_RAMP_TEX");
                _thresholdMaterial.EnableKeyword("USE_RAMP_TEX");
            }
            else
            {
                _ditherMaterial.DisableKeyword("USE_RAMP_TEX");
                _thresholdMaterial.DisableKeyword("USE_RAMP_TEX");
            }

            _superTarget = new RenderTargetIdentifier(_superID);
            _halfTarget = new RenderTargetIdentifier(_halfID);
        }

        public void SetColors(Color bg, Color fg)
        {
            _backgroundColor = bg;
            _foregroundColor = fg;

            if (_ditherMaterial != null)
            {
                _ditherMaterial.SetColor(_bgID, _backgroundColor);
                _ditherMaterial.SetColor(_fgID, _foregroundColor);
            }

            if (_thresholdMaterial == null)
            {
                _thresholdMaterial.SetColor(_bgID, _backgroundColor);
                _thresholdMaterial.SetColor(_fgID, _foregroundColor);
            }
        }

        // Called per-pass - Same as OnCameraSetup() below.
        // public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor) {}

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
            cmd.GetTemporaryRT(_superID, _targetsDescriptor, _filterMode);

            // _targetsDescriptor.width >>= 1;
            // _targetsDescriptor.height >>= 1;
            cmd.GetTemporaryRT(_halfID, _targetsDescriptor, _filterMode);

            if (!_worldSpaceDither)
            {
                var corners = new Vector3[4];
                renderingData.cameraData.camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1),
                    renderingData.cameraData.camera.farClipPlane,
                    Camera.MonoOrStereoscopicEye.Mono, corners);

                for (int i = 0; i < 4; i++)
                {
                    corners[i] = renderingData.cameraData.camera.transform.TransformVector(corners[i]);
                    corners[i].Normalize();
                }

                _ditherMaterial.SetVector(_blID, corners[0]);
                _ditherMaterial.SetVector(_tlID, corners[1]);
                _ditherMaterial.SetVector(_trID, corners[2]);
                _ditherMaterial.SetVector(_brID, corners[3]);
            }

            _ditherMaterial.SetFloat(_mgThresholdProperty, _middleColorThreshold);
            _ditherMaterial.SetFloat(_thresholdProperty, _threshold);
            _ditherMaterial.SetFloat(_tilingProperty, _tiling);

            _ditherMaterial.SetColor(_bgID, _backgroundColor);
            _ditherMaterial.SetColor(_mgID, _middleColor);
            _ditherMaterial.SetColor(_fgID, _foregroundColor);
            _thresholdMaterial.SetColor(_bgID, _backgroundColor);
            _thresholdMaterial.SetColor(_fgID, _foregroundColor);
        }

        public void SetTarget(RenderTargetIdentifier cameraColorTarget)
        {
            _colorTarget = cameraColorTarget;
        }

        // The actual execution of the pass. This is where custom rendering occurs.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, new ProfilingSampler(_profilerTag)))
            {
                Blit(cmd, _colorTarget, _superTarget, _ditherMaterial);
                Blit(cmd, _superTarget, _colorTarget);

                // Blit(cmd, _colorTarget, _superTarget);
                // Blit(cmd, _superTarget, _halfTarget, _ditherMaterial);
                // Blit(cmd, _halfTarget, _colorTarget);
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
            cmd.ReleaseTemporaryRT(_superID);
            cmd.ReleaseTemporaryRT(_halfID);
        }

        public void Dispose()
        {
            CoreUtils.Destroy(_ditherMaterial);
            CoreUtils.Destroy(_thresholdMaterial);
        }
    }
}