using DG.Tweening;
using UnityEngine;


// Flashing Light Script using Tween to Sequence. Works with any light as it simply Tweens between intensity.
namespace Lights
{
    [RequireComponent(typeof(Light))]
    public class FlashingLight : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Minimum intensity between which to animate the light.")]
        [Min(0.01f)]
        private float minIntensity = 7.0f;

        [SerializeField]
        [Tooltip("Maximum intensity between which to animate the light.")]
        private float maxIntensity = 10.0f;

        [SerializeField]
        [Tooltip("Duration of one cycle")]
        [Min(0.001f)]
        private float oneCycleDuration = 1.0f;

        [SerializeField]
        [Tooltip("The min intensities will randomly add onto the set minIntensity to achieve a variation")]
        private float minVariation;

        [SerializeField]
        [Tooltip("The max intensities will randomly add onto the set maxIntensity to achieve a variation")]
        private float maxVariation;

        [SerializeField]
        [Tooltip("Easing of the light")]
        private Ease easeType = Ease.OutFlash; // Outflash gives a heart beat type feel

        private Light _light;

        private Sequence _flashingLightLoop;

        private void Awake()
        {
            _light = GetComponent<Light>();

            _flashingLightLoop = DOTween.Sequence();
            // Set to minimum
            _light.intensity = minIntensity;
            // Set sequence to go max and then min, with random range in between, set infinite looping, set ease type

            _flashingLightLoop
                .AppendCallback(LightToMax)
                .AppendInterval(oneCycleDuration / 2.0f)
                .AppendCallback(LightToMin)
                .AppendInterval(oneCycleDuration / 2.0f)
                .SetLoops(-1)
                .SetEase(easeType);
        }

        private void OnValidate()
        {
            if (_flashingLightLoop != null)
            {
                _flashingLightLoop.Kill();

                _flashingLightLoop = DOTween.Sequence();
                // Set to minimum
                _light.intensity = minIntensity;
                // Set sequence to go max and then min, with random range in between, set infinite looping, set ease type

                _flashingLightLoop
                    .AppendCallback(LightToMax)
                    .AppendInterval(oneCycleDuration / 2.0f)
                    .AppendCallback(LightToMin)
                    .AppendInterval(oneCycleDuration / 2.0f)
                    .SetLoops(-1)
                    .SetEase(easeType);

                _flashingLightLoop.Play();
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            _flashingLightLoop.Play();
        }

        private void LightToMax()
        {
            _light.DOIntensity(Random.Range(maxIntensity, maxIntensity + maxVariation), oneCycleDuration / 2);
        }

        private void LightToMin()
        {
            _light.DOIntensity(Random.Range(minIntensity, minIntensity + minVariation), oneCycleDuration / 2);
        }
    }
}