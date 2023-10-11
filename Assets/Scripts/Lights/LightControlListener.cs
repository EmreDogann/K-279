using Rooms;
using UnityEngine;

namespace Lights
{
    public class LightControlListener : MonoBehaviour
    {
        [SerializeField] private ControlLight controlLight;

        private void OnEnable()
        {
            LightManager.OnLightControl += OnLightControl;
            Room.OnLightsSwitch += OnRoomLightControl;
        }

        private void OnDisable()
        {
            LightManager.OnLightControl -= OnLightControl;
            Room.OnLightsSwitch -= OnRoomLightControl;
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

        private void OnRoomLightControl(bool turnOn, float duration)
        {
            if (!controlLight.CanBeControlledByRoom())
            {
                return;
            }

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