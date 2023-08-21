using System;
using System.Collections;
using Audio;
using MyBox;
using RenderFeatures;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

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

    [InitializeOnLoad]
    public class LightManager : MonoBehaviour
    {
        [SerializeField] private bool setOnAwake;
        [ConditionalField(nameof(setOnAwake))] [SerializeField] private LightState startingState;

        [SerializeField] private Color normalColor;
        [SerializeField] private Color alarmColor;
        [SerializeField] private ScreenDitherRenderFeature _ditherRenderFeature;

        [SerializeField] private AudioSO alarmSound;

        private LightState _currentState;

        public static LightManager Instance { get; private set; }

        public static event Action<LightData> OnChangeColor;
        public static event Action<bool, float> OnLightControl;

        private LightData _originalColors;

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

            _originalColors = new LightData
            {
                BgColor = _ditherRenderFeature.GetBGColor(),
                FgColor = _ditherRenderFeature.GetFGColor()
            };

            if (setOnAwake)
            {
                _currentState = startingState;
                UpdateLightColor();
            }
        }

        private void OnDestroy()
        {
            _ditherRenderFeature.SetColors(_originalColors.BgColor, _originalColors.FgColor);
        }

        public void ToggleLights(bool isOn, float duration = 0.3f, float afterFadeWait = 1.1f)
        {
            StartCoroutine(TransitionLights(isOn, duration, afterFadeWait));
        }

        private IEnumerator TransitionLights(bool isOn, float duration, float afterFadeWait)
        {
            OnLightControl?.Invoke(isOn, duration);
            yield return new WaitForSecondsRealtime(afterFadeWait);
        }

        public void ChangeLightColor(LightState state, float lightFadeDuration = 0.3f, float afterFadeWait = 1.1f)
        {
            StartCoroutine(TransitionColors(lightFadeDuration, afterFadeWait));
            _currentState = state;
        }

        private IEnumerator TransitionColors(float lightFadeDuration, float afterFadeWait)
        {
            OnLightControl?.Invoke(false, lightFadeDuration);
            yield return new WaitForSecondsRealtime(afterFadeWait);

            UpdateLightColor();

            OnLightControl?.Invoke(true, lightFadeDuration);
            yield return new WaitForSecondsRealtime(afterFadeWait);
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

#if UNITY_EDITOR
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
            if (!EditorApplication.isPlaying)
            {
                _ditherRenderFeature.SetColors(Color.black, normalColor);
                return;
            }

            ChangeLightColor(LightState.Normal);
        }

        [ButtonMethod]
        private void SetAlarmState()
        {
            if (!EditorApplication.isPlaying)
            {
                _ditherRenderFeature.SetColors(Color.black, alarmColor);
                return;
            }

            ChangeLightColor(LightState.Alarm);
        }
#endif
    }
}