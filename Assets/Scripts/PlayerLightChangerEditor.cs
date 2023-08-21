using RenderFeatures;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerLightChangerEditor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ScreenDitherRenderFeature ditherRenderFeature;

    private Color _bgColor;
    private Color _fgColor;

    private static readonly int Bg = Shader.PropertyToID("_BG");
    private static readonly int Fg = Shader.PropertyToID("_FG");

    private void Update()
    {
        if (_bgColor != ditherRenderFeature.GetBGColor() || _fgColor != ditherRenderFeature.GetFGColor())
        {
            spriteRenderer.sharedMaterial.SetColor(Bg, ditherRenderFeature.GetBGColor());
            spriteRenderer.sharedMaterial.SetColor(Fg, ditherRenderFeature.GetFGColor());
        }
    }
}