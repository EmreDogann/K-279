using Rooms;
using UnityEngine;

namespace Lights
{
    public class PlayerLightControl : MonoBehaviour
    {
        [SerializeField] private RoomLight playerLight;

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
                playerLight.TurnOnLight(duration);
            }
            else
            {
                playerLight.TurnOffLight(duration);
            }
        }
    }
}