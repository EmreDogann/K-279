using MyBox;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Screen Shake Profile", menuName = "Screen Shake/New Profile", order = 0)]
    public class ScreenShakeProfile : ScriptableObject
    {
        [Separator("Impulse Source Settings")]
        public float impactTime = 0.2f;
        public float impactForce = 1.0f;
        public Vector3 defaultVelocity = new Vector3(0.0f, -1.0f, 0.0f);

        [Separator("Impulse Listener Settings")]
        public float listenerAmplitude = 1.0f;
        public float listenerFrequency = 1.0f;
        public float listenerDuration = 1.0f;
    }
}