using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace Inspect.Views.Transitions
{
    [Serializable]
    public class SlideMenuTransition : MenuTransition
    {
        private enum SlideDirection
        {
            UP,
            DOWN,
            RIGHT,
            LEFT
        }
        [SerializeField] private SlideDirection openDirection;
        [SerializeField] private SlideDirection closeDirection;
        [Space]
        [SerializeField] private float openDuration = 0.1f;
        [SerializeField] private float closeDuration = 0.1f;
        [Space]
        [SerializeField] private float openDistance = 0.5f;
        [SerializeField] private float closeDistance = 0.5f;
        [Space]
        [SerializeField] private Easing openEasing = Easing.Linear;
        [SerializeField] private Easing closeEasing = Easing.Linear;
        private RectTransform _rectTransform;

        private Vector2 _startingPosition;

        public override void Initialize(View view)
        {
            _rectTransform = view.GetComponent<RectTransform>();
            _startingPosition = _rectTransform.localPosition;

            view.gameObject.SetActive(false);
        }

        private IEnumerator Slide(Vector2 start, Vector2 end, float duration, Func<float, float> ease)
        {
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
        }

        public override IEnumerator Show(View view)
        {
            Vector2 position = _startingPosition - GetDirectionVector(openDirection) * openDistance;
            _rectTransform.localPosition = position;
            yield return Slide(position, _startingPosition, openDuration, openEasing.GetFunction());
        }

        public override IEnumerator Hide(View view)
        {
            yield return Slide(_startingPosition,
                _startingPosition + GetDirectionVector(closeDirection) * closeDistance, closeDuration,
                closeEasing.GetFunction());
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