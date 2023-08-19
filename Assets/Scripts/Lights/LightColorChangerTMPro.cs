using TMPro;
using UnityEngine;

namespace Lights
{
    public class LightColorChangerTMPro : LightColorChangeListener
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private bool useFgColor = true;

        public override void OnChangeColor(LightData data)
        {
            text.color = useFgColor ? data.FgColor : data.BgColor;
        }
    }
}