using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace RenderFeatures
{
    public class ScreenDitherRenderFeature : ScriptableRendererFeature
    {
        [SerializeField] private bool copyToCameraFramebuffer;
        [SerializeField] private bool showInSceneView;

        [SerializeField] private Texture2D ditherTex;
        [SerializeField] private Texture2D rampTex;
        [SerializeField] private float threshold = 0.1f;
        [SerializeField] private float tiling = 192.0f;
        [SerializeField] private FilterMode filterMode = FilterMode.Bilinear;

        // Where/when the render pass should be injected during the rendering process.
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingTransparents;

        private ScreenDitherRenderPass _activePass;

        // Gets called every time serialization happens.
        // Gets called when you enable/disable the renderer feature.
        // Gets called when you change a property in the inspector of the renderer feature.
        public override void Create()
        {
            name = "Screen Dither";
            _activePass = new ScreenDitherRenderPass("Screen-Space Dither", renderPassEvent, ditherTex, rampTex,
                threshold, tiling, filterMode);
        }

        // Injects one or multiple render passes in the renderer.
        // Gets called when setting up the renderer, once per-camera.
        // Gets called every frame, once per-camera.
        // Will not be called if the renderer feature is disabled in the renderer inspector.
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            // Register our blur pass to the scriptable renderer.

            if (copyToCameraFramebuffer && showInSceneView)
            {
                // _activePass.ConfigureInput(ScriptableRenderPassInput.Color);
                // _activePass.SetTarget(renderer.cameraColorTargetHandle);
                renderer.EnqueuePass(_activePass);
            }
            else if (renderingData.cameraData.cameraType == CameraType.Game)
            {
                // https://forum.unity.com/threads/how-to-blit-in-urp-documentation-unity-blog-post-on-every-blit-function.1211508/#post-8375610
                // You can use ConfigureInput(Color); to make the opaque texture available in your scriptable render pass (regardless of what the renderer asset settings are).
                // _activePass.ConfigureInput(ScriptableRenderPassInput.Color);
                // _activePass.SetTarget(renderer.cameraColorTargetHandle);

                renderer.EnqueuePass(_activePass);
            }
        }

        public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
        {
            if (copyToCameraFramebuffer && showInSceneView)
            {
                _activePass.ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Normal |
                                           ScriptableRenderPassInput.Depth);
                _activePass.SetTarget(renderer.cameraColorTargetHandle);
            }
            else if (renderingData.cameraData.cameraType == CameraType.Game)
            {
                // https://forum.unity.com/threads/how-to-blit-in-urp-documentation-unity-blog-post-on-every-blit-function.1211508/#post-8375610
                // You can use ConfigureInput(Color); to make the opaque texture available in your scriptable render pass (regardless of what the renderer asset settings are).
                _activePass.ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Normal |
                                           ScriptableRenderPassInput.Depth);
                _activePass.SetTarget(renderer.cameraColorTargetHandle);
            }
        }
    }
}