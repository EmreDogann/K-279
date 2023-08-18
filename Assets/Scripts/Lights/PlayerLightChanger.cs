using UnityEngine;

namespace Lights
{
    public class PlayerLightChanger : LightColorChangeListener
    {
        [SerializeField] private SpriteRenderer _renderer;
        private static readonly int Bg = Shader.PropertyToID("_BG");
        private static readonly int Fg = Shader.PropertyToID("_FG");

        public override void OnChangeColor(LightData data)
        {
            _renderer.sharedMaterial.SetColor(Bg, data.BgColor);
            _renderer.sharedMaterial.SetColor(Fg, data.FgColor);
        }
    }
}