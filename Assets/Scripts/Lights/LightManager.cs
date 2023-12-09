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

    public enum LightChangeMode
    {
        LeaveAsIs,
        Off,
        On
    }

    public class LightData
    {
        public LightState State;
        public Color BgColor;
        public Color FgColor;
    }

#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class LightManager : MonoBehaviour
    {
        [SerializeField] private LightState startingState;

        [SerializeField] private Color normalColor;
        [SerializeField] private Color alarmColor;
        [SerializeField] private ScreenDitherRenderFeature ditherRenderFeature;

        [SerializeField] private AudioSO alarmSound;

        private LightState _currentState;

        public static LightManager Instance { get; private set; }

        public static event Action<LightData> OnChangeState;
        public static event Action<bool, float> OnLightControl;

        private LightData _originalColors;

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
        }

        private void Start()
        {
            _originalColors = new LightData
            {
                BgColor = ditherRenderFeature.GetBGColor(),
                FgColor = ditherRenderFeature.GetFGColor()
            };

            ChangeState(startingState, LightChangeMode.LeaveAsIs, 0.0f, 0.0f);
        }

        private void OnDestroy()
        {
            ditherRenderFeature.SetColors(_originalColors.BgColor, _originalColors.FgColor);
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

        public void ChangeState(LightState state, LightChangeMode changeMode = LightChangeMode.LeaveAsIs,
            float lightFadeDuration = 0.3f,
            float afterFadeWait = 1.1f)
        {
            _currentState = state;
            StartCoroutine(TransitionColors(changeMode, lightFadeDuration, afterFadeWait));
        }

        private IEnumerator TransitionColors(LightChangeMode changeMode, float lightFadeDuration, float afterFadeWait)
        {
            if (changeMode == LightChangeMode.Off || changeMode == LightChangeMode.On)
            {
                OnLightControl?.Invoke(false, lightFadeDuration);
                if (afterFadeWait > 0.0f)
                {
                    yield return new WaitForSecondsRealtime(afterFadeWait);
                }
            }

            UpdateLightColor();

            if (changeMode == LightChangeMode.On)
            {
                OnLightControl?.Invoke(true, lightFadeDuration);
                if (afterFadeWait > 0.0f)
                {
                    yield return new WaitForSecondsRealtime(afterFadeWait);
                }
            }
        }

        private void UpdateLightColor()
        {
            LightData lightData = new LightData { State = _currentState };
            switch (_currentState)
            {
                case LightState.Normal:
                    lightData.BgColor = Color.black;
                    lightData.FgColor = normalColor;
                    ditherRenderFeature.SetColors(Color.black, normalColor);
                    alarmSound.Stop(true, 0.4f);
                    break;
                case LightState.Alarm:
                    lightData.BgColor = Color.black;
                    lightData.FgColor = alarmColor;
                    ditherRenderFeature.SetColors(Color.black, alarmColor);
                    alarmSound.StopAll();
                    alarmSound.Play2D();
                    break;
            }

            OnChangeState?.Invoke(lightData);
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
                ditherRenderFeature.SetColors(Color.black, normalColor);
                return;
            }

            ChangeState(LightState.Normal);
        }

        [ButtonMethod]
        private void SetAlarmState()
        {
            if (!EditorApplication.isPlaying)
            {
                ditherRenderFeature.SetColors(Color.black, alarmColor);
                return;
            }

            ChangeState(LightState.Alarm);
        }
#endif
    }
}