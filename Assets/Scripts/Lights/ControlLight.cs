using UnityEngine;

namespace Lights
{
    public abstract class ControlLight : MonoBehaviour
    {
        public abstract bool IsOn();
        public abstract bool CanBeControlledByRoom();
        public abstract void TurnOffLight(float duration = 0.0f);
        public abstract void TurnOnLight(float duration = 0.0f);
        public abstract void ChangeLightState(LightState state);
    }
}