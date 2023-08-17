using System;
using System.Collections;
using MyBox;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace UI
{
    public class CanvasGroupFade : MonoBehaviour
    {
        private enum FadeType
        {
            FadeIn,
            FadeOut
        }

        [SerializeField] private FadeType fadeType;
        [SerializeField] private bool fadeOnAwake;
        [SerializeField] private bool blockRaycastsWhenFading;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float beforeWaitTime;
        [SerializeField] private float afterWaitTime;

        [Separator("Fade Settings")]
        [SerializeField] private float duration;
        // [SerializeField] private float startValue;
        // [SerializeField] private float endValue;
        [SerializeField] private Easing easingFunction;

        [Separator("Events")]
        public UnityEvent OnFadeStart;
        public UnityEvent OnFadeComplete;

        private bool _isCanvasActive;

        private void Start()
        {
            if (fadeOnAwake)
            {
                StartFade();
            }
        }

        public void StartFade()
        {
            if (blockRaycastsWhenFading)
            {
                _canvasGroup.blocksRaycasts = true;
            }

            if (fadeType == FadeType.FadeIn)
            {
                StartCoroutine(FadeIn());
            }
            else
            {
                StartCoroutine(FadeOut());
            }
        }

        public Coroutine ToggleFade(float beforeWait = -1, float afterWait = -1)
        {
            if (_isCanvasActive)
            {
                return StartCoroutine(FadeOut(beforeWait, afterWait));
            }

            return StartCoroutine(FadeIn(beforeWait, afterWait));
        }

        private IEnumerator FadeIn(float beforeWait = -1, float afterWait = -1)
        {
            if (beforeWait < 0.0f)
            {
                beforeWait = beforeWaitTime;
            }

            if (afterWait < 0.0f)
            {
                afterWait = afterWaitTime;
            }

            OnFadeStart?.Invoke();

            _canvasGroup.alpha = 0.0f;
            yield return new WaitForSecondsRealtime(beforeWait);
            yield return Fade(0.0f, 1.0f, duration, easingFunction.GetFunction());
            _canvasGroup.blocksRaycasts = true;
            _isCanvasActive = true;

            yield return new WaitForSecondsRealtime(afterWait);
            OnFadeComplete?.Invoke();
        }

        private IEnumerator FadeOut(float beforeWait = -1, float afterWait = -1)
        {
            if (beforeWait < 0.0f)
            {
                beforeWait = beforeWaitTime;
            }

            if (afterWait < 0.0f)
            {
                afterWait = afterWaitTime;
            }

            OnFadeStart?.Invoke();

            _canvasGroup.alpha = 1.0f;
            yield return new WaitForSecondsRealtime(beforeWait);
            yield return Fade(1.0f, 0.0f, duration, easingFunction.GetFunction());
            _canvasGroup.blocksRaycasts = false;
            _isCanvasActive = false;

            yield return new WaitForSecondsRealtime(afterWait);
            OnFadeComplete?.Invoke();
        }

        private IEnumerator Fade(float start, float end, float duration, Func<float, float> ease)
        {
            float current = _canvasGroup.alpha;
            float elapsedTime = Mathf.InverseLerp(start, end, current) * duration;

            while (elapsedTime < duration)
            {
                _canvasGroup.alpha = Mathf.Lerp(start, end, ease(elapsedTime / duration));
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            _canvasGroup.alpha = end;
        }
    }
}