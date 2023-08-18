using DG.Tweening;
using MyBox;
using UnityEngine;

namespace Lights
{
    [RequireComponent(typeof(Light))]
    public class RoomLight : MonoBehaviour
    {
        // TODO: Light Flickering
        [SerializeField] private bool flickering;
        [ConditionalField(nameof(flickering))] [SerializeField] private bool flickeringAmplitude;
        [ConditionalField(nameof(flickering))] [SerializeField] private bool flickeringFrequency;
        [ConditionalField(nameof(flickering))] [SerializeField] private bool flickeringCooldown;

        private Light _light;
        private float _originalIntensity;

        private void Awake()
        {
            _light = GetComponent<Light>();
            _originalIntensity = _light.intensity;
        }

        public bool IsOn()
        {
            return _light.intensity > 0;
        }

        public void TurnOffLight(float duration = 0.0f)
        {
            if (duration <= 0.0f)
            {
                _light.intensity = 0.0f;
            }
            else
            {
                _light.DOIntensity(0.0f, duration);
            }
        }

        public void TurnOnLight(float duration = 0.0f)
        {
            if (duration <= 0.0f)
            {
                _light.intensity = _originalIntensity;
            }
            else
            {
                _light.DOIntensity(_originalIntensity, duration);
            }
        }
    }
}