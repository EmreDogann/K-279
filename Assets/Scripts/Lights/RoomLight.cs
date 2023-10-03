using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MyBox;
using UnityEngine;

namespace Lights
{
    [RequireComponent(typeof(Light))]
    public class RoomLight : MonoBehaviour
    {
        [SerializeField] private bool roomCanControl = true;
        [SerializeField] private bool canBeAlarmLight;
        [ConditionalField(nameof(canBeAlarmLight))] [SerializeField] private bool isStaticAlarm;

        private Light _light;
        private float _originalIntensity;
        private Tween _rotateLights;

        private List<ILightEffect> _lightEffects = new List<ILightEffect>();

        private void Awake()
        {
            _light = GetComponent<Light>();

            _lightEffects = GetComponents<ILightEffect>().ToList();

            _originalIntensity = _light.intensity;

            _rotateLights = transform.DORotate(new Vector3(0.0f, 360.0f, 0.0f), 1.0f, RotateMode.WorldAxisAdd)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .SetAutoKill(false)
                .Pause();
        }

        public bool IsOn()
        {
            return _light.intensity > 0;
        }

        public bool CanBeControlledByRoom()
        {
            return roomCanControl;
        }

        public void TurnOffLight(float duration = 0.0f)
        {
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

        public void TurnOnLight(float duration = 0.0f)
        {
            if (LightManager.Instance.GetLightState() == LightState.Alarm)
            {
                if (!canBeAlarmLight)
                {
                    return;
                }

                if (!isStaticAlarm)
                {
                    _rotateLights.Rewind();
                    _rotateLights.PlayForward();
                }
            }

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
    }
}