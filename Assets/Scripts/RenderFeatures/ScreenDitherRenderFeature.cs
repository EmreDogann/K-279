using System;
using MyBox;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace RenderFeatures
{
    public class ScreenDitherRenderFeature : ScriptableRendererFeature
    {
        [Serializable]
        public class Settings
        {
            // [Header("Draw Renderers Settings")]
            // public LayerMask layerMask = 1;

            public bool showInSceneView;
            public bool worldSpaceDither;

            [Space]
            public Texture2D blueDitherTex;
            public Texture2D whiteDitherTex;
            public Texture2D interleavedGradientDitherTex;
            public Texture2D bayerDitherTex;
            public bool useRampTexture;
            [ConditionalField(nameof(useRampTexture))] public Texture2D rampTex;

            [Space]
            [ConditionalField(nameof(useRampTexture), true)] public Color backgroundColor;
            [ConditionalField(nameof(useRampTexture), true)] public Color middleColor;
            [ConditionalField(nameof(useRampTexture), true)] public Color foregroundColor;

            [Space]
            [Range(0.0f, 1.0f)] public float middleColorThreshold = 0.15f;
            public float tiling = 192.0f;
            [HideInInspector] public float threshold = 0.1f;                      // Not used.
            [HideInInspector] public FilterMode filterMode = FilterMode.Bilinear; // Not used.
        }

        [SerializeField] private Settings settings = new Settings();

        // Where/when the render pass should be injected during the rendering process.
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingTransparents;

        private readonly bool copyToCameraFramebuffer = true;
        private ScreenDitherRenderPass _activePass;

        // Gets called every time serialization happens.
        // Gets called when you enable/disable the renderer feature.
        // Gets called when you change a property in the inspector of the renderer feature.
        public override void Create()
        {
            name = "Screen Dither";
            _activePass = new ScreenDitherRenderPass("Screen-Space Dither", renderPassEvent, settings);
        }

        // Injects one or multiple render passes in the renderer.
        // Gets called when setting up the renderer, once per-camera.
        // Gets called every frame, once per-camera.
        // Will not be called if the renderer feature is disabled in the renderer inspector.
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            // Register our blur pass to the scriptable renderer.

            if (copyToCameraFramebuffer && settings.showInSceneView)
            {
                renderer.EnqueuePass(_activePass);
            }
            else if (renderingData.cameraData.cameraType == CameraType.Game)
            {
                // https://forum.unity.com/threads/how-to-blit-in-urp-documentation-unity-blog-post-on-every-blit-function.1211508/#post-8375610
                // You can use ConfigureInput(Color); to make the opaque texture available in your scriptable render pass (regardless of what the renderer asset settings are).

                renderer.EnqueuePass(_activePass);
            }
        }

        public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
        {
            if (copyToCameraFramebuffer && settings.showInSceneView)
            {
                if (settings.worldSpaceDither)
                {
                    _activePass.ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Normal |
                                               ScriptableRenderPassInput.Depth);
                }
                else
                {
                    _activePass.ConfigureInput(ScriptableRenderPassInput.Color);
                }

                _activePass.SetTarget(renderer.cameraColorTargetHandle);
            }
            else if (renderingData.cameraData.cameraType == CameraType.Game)
            {
                // https://forum.unity.com/threads/how-to-blit-in-urp-documentation-unity-blog-post-on-every-blit-function.1211508/#post-8375610
                // You can use ConfigureInput(Color); to make the opaque texture available in your scriptable render pass (regardless of what the renderer asset settings are).
                if (settings.worldSpaceDither)
                {
                    _activePass.ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Normal |
                                               ScriptableRenderPassInput.Depth);
                }
                else
                {
                    _activePass.ConfigureInput(ScriptableRenderPassInput.Color);
                }

                _activePass.SetTarget(renderer.cameraColorTargetHandle);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _activePass.ReleaseTargets();
        }

        public void SetColors(Color bg, Color fg)
        {
            settings.backgroundColor = bg;
            settings.foregroundColor = fg;
            _activePass.SetColors(bg, fg);
        }

        public Color GetBGColor()
        {
            return settings.backgroundColor;
        }

        public void SetBGColors(Color bg)
        {
            settings.backgroundColor = bg;
            _activePass.SetColors(bg, settings.foregroundColor);
        }

        public Color GetFGColor()
        {
            return settings.foregroundColor;
        }

        public void SetFGColors(Color fg)
        {
            settings.foregroundColor = fg;
            _activePass.SetColors(settings.backgroundColor, fg);
        }
    }
}