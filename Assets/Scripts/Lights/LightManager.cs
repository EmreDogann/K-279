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
        Off,
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
        public static event Action<LightState> OnLightStateChange;

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
                OnLightStateChange?.Invoke(_currentState);
                UpdateLightColor();
            }
        }

        [ButtonMethod]
        public void SetOffState()
        {
            SetLightState(LightState.Off);
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
            StartCoroutine(TransitionColors(state));
            OnLightStateChange?.Invoke(state);
            _currentState = state;
        }

        private IEnumerator TransitionColors(LightState newState)
        {
            LightControl.OnLightControl?.Invoke(false, 0.3f);
            yield return new WaitForSecondsRealtime(1.1f);
            UpdateLightColor();

            if (newState != LightState.Off)
            {
                LightControl.OnLightControl?.Invoke(true, 0.3f);
                yield return new WaitForSecondsRealtime(0.3f);
            }
        }

        private void UpdateLightColor()
        {
            LightData lightData = new LightData { State = _currentState };
            switch (_currentState)
            {
                case LightState.Off:
                    alarmSound.Stop(true, 0.4f);
                    return;
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