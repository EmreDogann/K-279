using UnityEngine;
using UnityEngine.UI;

namespace Lights
{
    public class LightColorChangerUI : LightColorChangeListener
    {
        [SerializeField] private Image image;
        [SerializeField] private bool useFgColor = true;

        public override void OnChangeColor(LightData data)
        {
            image.color = useFgColor ? data.FgColor : data.BgColor;
        }
    }
}