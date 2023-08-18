using System;
using RenderFeatures;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Lights
{
    public enum LightState
    {
        Normal,
        Alarm
    }

    public class LightData
    {
        public LightState State;
        public Color BgColor;
        public Color FgColor;
    }

    public class LightManager : MonoBehaviour
    {
        [SerializeField] private Color normalColor;
        [SerializeField] private Color alarmColor;
        [SerializeField] private UniversalRendererData _rendererData;

        private LightState _currentState;
        private ScreenDitherRenderFeature _ditherRenderFeature;

        public static LightManager Instance { get; private set; }

        public static event Action<LightData> OnChangeColor;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _ditherRenderFeature =
                _rendererData.rendererFeatures.Find(x => x.GetType() == typeof(ScreenDitherRenderFeature)) as
                    ScreenDitherRenderFeature;
        }

        public void SetLightState(LightState state)
        {
            LightData lightData = new LightData { State = state };
            switch (state)
            {
                case LightState.Normal:
                    lightData.BgColor = Color.black;
                    lightData.FgColor = normalColor;
                    _ditherRenderFeature.SetColors(Color.black, normalColor);
                    break;
                case LightState.Alarm:
                    lightData.BgColor = Color.black;
                    lightData.FgColor = alarmColor;
                    _ditherRenderFeature.SetColors(Color.black, alarmColor);
                    break;
            }

            OnChangeColor?.Invoke(lightData);
            _currentState = state;
        }

        public LightState GetLightState()
        {
            return _currentState;
        }
    }
}