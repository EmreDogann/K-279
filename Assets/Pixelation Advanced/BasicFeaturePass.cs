using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
    public class BasicFeaturePass : ScriptableRenderPass
    {
        private readonly Material blitMat;
        private readonly float pixelDensity;

        private readonly ProfilingSampler m_ProfilingSampler;
        private RenderStateBlock m_RenderStateBlock;
        private readonly List<ShaderTagId> m_ShaderTagIdList = new List<ShaderTagId>();
        private FilteringSettings m_FilteringSettings;

        private static readonly int pixelTexID = Shader.PropertyToID("_PixelTexture");
        private static readonly int pixelDepthID = Shader.PropertyToID("_DepthTex");

        public BasicFeaturePass(RenderPassEvent renderEvent, Material bM, float pD, int lM)
        {
            m_ProfilingSampler = new ProfilingSampler("BasicFeature");
            renderPassEvent = renderEvent;
            blitMat = bM;
            pixelDensity = pD;
            blitMat.SetFloat("_PixelDensity", pixelDensity);

            m_FilteringSettings = new FilteringSettings(RenderQueueRange.opaque, lM);

            m_ShaderTagIdList.Add(new ShaderTagId("UniversalForward"));
            m_ShaderTagIdList.Add(new ShaderTagId("LightweightForward"));
            m_ShaderTagIdList.Add(new ShaderTagId("SRPDefaultUnlit"));

            m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            SortingCriteria sortingCriteria = SortingCriteria.CommonTransparent;

            //Get the camera info of the default rendering pass
            ScriptableRenderer renderer = renderingData.cameraData.renderer;
            RenderTargetIdentifier colorHandle = renderer.cameraColorTargetHandle;
            RenderTargetIdentifier depthHandle = renderer.cameraDepthTargetHandle;

            //Generate necessary data for the pixel renderer
            DrawingSettings drawingSettings =
                CreateDrawingSettings(m_ShaderTagIdList, ref renderingData, sortingCriteria);
            ref CameraData cameraData = ref renderingData.cameraData;
            Camera camera = cameraData.camera;
            int pixelWidth = (int)(camera.pixelWidth / pixelDensity);
            int pixelHeight = (int)(camera.pixelHeight / pixelDensity);
            CommandBuffer cmd = CommandBufferPool.Get();

            using (new ProfilingScope(cmd, new ProfilingSampler("BasicFeature")))
            {
                //set the render target and buffers for the pixel renderer
                cmd.GetTemporaryRT(pixelTexID, pixelWidth, pixelHeight, 0, FilterMode.Point);
                cmd.GetTemporaryRT(pixelDepthID, pixelWidth, pixelHeight, 24, FilterMode.Point,
                    RenderTextureFormat.Depth);
                cmd.SetRenderTarget(pixelTexID, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store,
                    pixelDepthID, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
                //clear screen
                cmd.ClearRenderTarget(true, true, Color.clear);
                //do all of the above and start fresh
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();
                //draw the pixel pixel renderer
                context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref m_FilteringSettings,
                    ref m_RenderStateBlock);
                //set the render texture back to the default to blend correctly
                cmd.SetRenderTarget(colorHandle, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store,
                    depthHandle, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
                //render everything with the blit shader
                cmd.Blit(new RenderTargetIdentifier(pixelTexID), BuiltinRenderTextureType.CurrentActive, blitMat);
                //remove buffers to not have memoryLeak
                cmd.ReleaseTemporaryRT(pixelTexID);
                cmd.ReleaseTemporaryRT(pixelDepthID);
            }

            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }
    }
}