using UnityEngine;

namespace Lights
{
    public class PlayerLightControl : MonoBehaviour
    {
        [SerializeField] private RoomLight playerLight;

        private void OnEnable()
        {
            LightControl.OnLightControl += OnLightControl;
        }

        private void OnDisable()
        {
            LightControl.OnLightControl -= OnLightControl;
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