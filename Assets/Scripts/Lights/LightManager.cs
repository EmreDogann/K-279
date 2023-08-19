using System;
using System.Collections;
using MyBox;
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
        [SerializeField] private bool setOnAwake;
        [ConditionalField(nameof(setOnAwake))] [SerializeField] private LightState startingState;

        [SerializeField] private Color normalColor;
        [SerializeField] private Color alarmColor;
        [SerializeField] private UniversalRendererData _rendererData;

        private LightState _currentState;
        private ScreenDitherRenderFeature _ditherRenderFeature;

        public static LightManager Instance { get; private set; }

        public static event Action<LightData> OnChangeColor;
        public static event Action OnLightStateChange;

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

            if (setOnAwake)
            {
                SetLightState(startingState);
            }
        }

        [ButtonMethod]
        public void SetNormalState()
        {
            SetLightState(LightState.Normal);
        }

        [ButtonMethod]
        public void SetAlarmState()
        {
            SetLightState(LightState.Alarm);
        }

        public void SetLightState(LightState state)
        {
            StartCoroutine(TransitionColors());
            OnLightStateChange?.Invoke();
            _currentState = state;
        }

        private IEnumerator TransitionColors()
        {
            LightControl.OnLightControl?.Invoke(false, 0.3f);
            yield return new WaitForSecondsRealtime(1.1f);
            UpdateLightColor();
            LightControl.OnLightControl?.Invoke(true, 0.3f);
            yield return new WaitForSecondsRealtime(0.3f);
        }

        private void UpdateLightColor()
        {
            LightData lightData = new LightData { State = _currentState };
            switch (_currentState)
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
        }

        public LightState GetLightState()
        {
            return _currentState;
        }
    }
}