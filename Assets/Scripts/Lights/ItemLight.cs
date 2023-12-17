using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Lights
{
    [RequireComponent(typeof(Light))]
    public class ItemLight : ControlLight
    {
        [SerializeField] private bool roomCanControl;

        private Light _light;
        private float _originalIntensity;

        private List<ILightEffect> _lightEffects = new List<ILightEffect>();

        private void Awake()
        {
            _light = GetComponent<Light>();
            _lightEffects = GetComponents<ILightEffect>().ToList();
            _originalIntensity = _light.intensity;
        }

        private void OnDestroy()
        {
            _light.DOKill();
        }

        public override bool IsOn()
        {
            return _light.intensity > 0;
        }

        public override bool CanBeControlledByRoom()
        {
            return roomCanControl;
        }

        public override void TurnOffLight(float duration = 0.0f)
        {
            _light.DOKill();

            if (duration <= 0.0f)
            {
                _light.intensity = 0.0f;
            }
            else
            {
                _light.DOIntensity(0.0f, duration).SetUpdate(true);
            }

            if (_lightEffects.Count > 0)
            {
                foreach (ILightEffect lightEffect in _lightEffects)
                {
                    lightEffect.DisableEffect();
                }
            }
        }

        public override void TurnOnLight(float duration = 0.0f)
        {
            _light.DOKill();

            if (duration <= 0.0f)
            {
                _light.intensity = _originalIntensity;
            }
            else
            {
                _light.DOIntensity(_originalIntensity, duration).SetUpdate(true);
            }

            if (_lightEffects.Count > 0)
            {
                foreach (ILightEffect lightEffect in _lightEffects)
                {
                    lightEffect.EnableEffect();
                }
            }
        }

        public override void ChangeLightState(LightState state) {}
    }
}