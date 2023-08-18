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
            public bool showInSceneView;
            public bool worldSpaceDither;

            public Texture2D ditherTex;
            public bool useRampTexture;
            [ConditionalField(nameof(useRampTexture))] public Texture2D rampTex;

            [ConditionalField(nameof(useRampTexture), true)] public Color backgroundColor;
            [ConditionalField(nameof(useRampTexture), true)] public Color foregroundColor;

            public float threshold = 0.1f;
            public float tiling = 192.0f;
            public FilterMode filterMode = FilterMode.Bilinear;
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

        public void SetColors(Color bg, Color fg)
        {
            settings.backgroundColor = bg;
            settings.foregroundColor = fg;
            _activePass.SetColors(bg, fg);
        }

        public void SetBGColors(Color bg)
        {
            settings.backgroundColor = bg;
            _activePass.SetColors(bg, settings.foregroundColor);
        }

        public void SetFGColors(Color fg)
        {
            settings.foregroundColor = fg;
            _activePass.SetColors(settings.backgroundColor, fg);
        }
    }
}