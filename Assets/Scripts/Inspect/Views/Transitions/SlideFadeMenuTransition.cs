using System;
using System.Collections;
using MyBox;
using UnityEngine;
using Utils;

namespace Inspect.Views.Transitions
{
    [Serializable]
    public class SlideFadeMenuTransition : MenuTransition
    {
        private enum SlideDirection
        {
            UP,
            DOWN,
            RIGHT,
            LEFT
        }
        [Separator("Slide Settings")]
        [OverrideLabel("Open Direction")] [SerializeField] private SlideDirection slide_openDirection;
        [OverrideLabel("Close Direction")] [SerializeField] private SlideDirection slide_closeDirection;
        [Space]
        [OverrideLabel("Open Duration")] [SerializeField] private float slide_openDuration = 0.1f;
        [OverrideLabel("Close Duration")] [SerializeField] private float slide_closeDuration = 0.1f;
        [Space]
        [OverrideLabel("Open Distance")] [SerializeField] private float slide_openDistance = 0.5f;
        [OverrideLabel("Close Distance")] [SerializeField] private float slide_closeDistance = 0.5f;
        [Space]
        [OverrideLabel("Open Easing")] [SerializeField] private Easing slide_openEasing = Easing.Linear;
        [OverrideLabel("Close Easing")] [SerializeField] private Easing slide_closeEasing = Easing.Linear;

        [Separator("Fade Settings")]
        [OverrideLabel("Open Duration")] [SerializeField] private float fade_openDuration = 0.1f;
        [OverrideLabel("Close Duration")] [SerializeField] private float fade_closeDuration = 0.1f;
        [Space]
        [OverrideLabel("Open Easing")] [SerializeField] private Easing fade_openEasing = Easing.Linear;
        [OverrideLabel("Close Easing")] [SerializeField] private Easing fade_closeEasing = Easing.Linear;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        private Vector2 _startingPosition;
        private int _coroutinesRunning;

        public override void Initialize(View view)
        {
            _canvasGroup = view.CanvasGroup;
            _rectTransform = view.GetComponent<RectTransform>();
            _startingPosition = _rectTransform.localPosition;

            view.gameObject.SetActive(false);
            _canvasGroup.alpha = 0.0f;
        }

        private IEnumerator Slide(Vector2 start, Vector2 end, float duration, Func<float, float> ease)
        {
            _coroutinesRunning++;
            Vector2 current = _rectTransform.localPosition;
            float elapsedTime = InverseLerp(start, end, current) * duration;

            while (elapsedTime < duration)
            {
                current = Vector2.Lerp(start, end, ease(elapsedTime / duration));
                _rectTransform.localPosition = current;

                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            _rectTransform.localPosition = end;
            _coroutinesRunning--;
        }

        private IEnumerator Fade(float start, float end, float duration, Func<float, float> ease)
        {
            _coroutinesRunning++;
            float current = _canvasGroup.alpha;
            float elapsedTime = Mathf.InverseLerp(start, end, current) * duration;

            while (elapsedTime < duration)
            {
                _canvasGroup.alpha = Mathf.Lerp(start, end, ease(elapsedTime / duration));
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            _canvasGroup.alpha = end;
            _coroutinesRunning--;
        }

        public override IEnumerator Show(View view)
        {
            IEnumerator fade = Fade(_canvasGroup.alpha, 1.0f, fade_openDuration, fade_openEasing.GetFunction());

            Vector2 position = _startingPosition - GetDirectionVector(slide_openDirection) * slide_openDistance;
            _rectTransform.localPosition = position;
            IEnumerator slide = Slide(position, _startingPosition, slide_openDuration, slide_openEasing.GetFunction());

            slide.MoveNext();
            fade.MoveNext();
            while (_coroutinesRunning > 0)
            {
                yield return null;
                slide.MoveNext();
                fade.MoveNext();
            }
        }

        public override IEnumerator Hide(View view)
        {
            IEnumerator fade = Fade(_canvasGroup.alpha, 0.0f, fade_closeDuration, fade_closeEasing.GetFunction());
            IEnumerator slide = Slide(_startingPosition,
                _startingPosition + GetDirectionVector(slide_closeDirection) * slide_closeDistance, slide_closeDuration,
                slide_closeEasing.GetFunction());

            slide.MoveNext();
            fade.MoveNext();
            while (_coroutinesRunning > 0)
            {
                yield return null;
                slide.MoveNext();
                fade.MoveNext();
            }
        }

        private Vector2 GetDirectionVector(SlideDirection direction)
        {
            switch (direction)
            {
                case SlideDirection.UP:
                    return Vector2.up;
                case SlideDirection.DOWN:
                    return Vector2.down;
                case SlideDirection.LEFT:
                    return Vector2.left;
                case SlideDirection.RIGHT:
                    return Vector2.right;
                default:
                    return Vector2.zero;
            }
        }

        public static float InverseLerp(Vector2 a, Vector2 b, Vector2 value)
        {
            Vector2 AB = b - a;
            Vector2 AV = value - a;
            return Vector2.Dot(AV, AB) / Vector2.Dot(AB, AB);
        }
    }
}