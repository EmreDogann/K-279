using System;
using System.Collections;
using Audio;
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

        [SerializeField] private AudioSO alarmSound;

        private LightState _currentState;
        private ScreenDitherRenderFeature _ditherRenderFeature;

        public static LightManager Instance { get; private set; }

        public static event Action<LightData> OnChangeColor;
        public static event Action<bool, float> OnLightControl;

        private void Start()
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
                _currentState = startingState;
                UpdateLightColor();
            }
        }

        [ButtonMethod]
        private void TurnOffLights()
        {
            ToggleLights(false);
        }

        [ButtonMethod]
        private void TurnOnLights()
        {
            ToggleLights(true);
        }

        [ButtonMethod]
        private void SetNormalState()
        {
            ChangeLightColor(LightState.Normal);
        }

        [ButtonMethod]
        private void SetAlarmState()
        {
            ChangeLightColor(LightState.Alarm);
        }

        public void ToggleLights(bool isOn, float duration = 0.3f)
        {
            StartCoroutine(TransitionLights(isOn, duration));
        }

        private IEnumerator TransitionLights(bool isOn, float duration)
        {
            OnLightControl?.Invoke(isOn, duration);
            yield return new WaitForSecondsRealtime(1.1f);
        }

        public void ChangeLightColor(LightState state, float duration = 0.3f)
        {
            StartCoroutine(TransitionColors(state, duration));
            _currentState = state;
        }

        private IEnumerator TransitionColors(LightState newState, float duration)
        {
            OnLightControl?.Invoke(false, duration);
            yield return new WaitForSecondsRealtime(1.1f);

            UpdateLightColor();

            OnLightControl?.Invoke(true, duration);
            yield return new WaitForSecondsRealtime(1.1f);
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
                    alarmSound.Stop(true, 0.4f);
                    break;
                case LightState.Alarm:
                    lightData.BgColor = Color.black;
                    lightData.FgColor = alarmColor;
                    _ditherRenderFeature.SetColors(Color.black, alarmColor);
                    alarmSound.Play2D();
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