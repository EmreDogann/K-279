using Rooms;
using UnityEngine;

namespace Lights
{
    public class LightControlListener : MonoBehaviour
    {
        [SerializeField] private RoomLight controlLight;

        private void OnEnable()
        {
            LightManager.OnLightControl += OnLightControl;
            Room.OnLightsSwitch += OnLightControl;
        }

        private void OnDisable()
        {
            LightManager.OnLightControl -= OnLightControl;
            Room.OnLightsSwitch -= OnLightControl;
        }

        private void OnLightControl(bool turnOn, float duration)
        {
            if (turnOn)
            {
                controlLight.TurnOnLight(duration);
            }
            else
            {
                controlLight.TurnOffLight(duration);
            }
        }
    }
}