using System;
using System.Collections.Generic;
using Audio;
using MyBox;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public class LightFlickerAudio
    {
        public AudioSO audio;
        [Tooltip("The minimum difference in intensity to trigger a zap. Set to 0 to always play a zap.")]
        public float threshold = 50.0f;
    }

    [CreateAssetMenu(fileName = "New Light Flicker Settings", menuName = "Light Flicker", order = 0)]
    public class LightFlickerSO : ScriptableObject
    {
        [Tooltip("Minimum random light intensity")]
        public float minIntensity;
        [Tooltip("Maximum random light intensity")]
        public float maxIntensity = 1f;
        [Tooltip("How often to flick per second during an iteration")]
        public float frequency = 0.1f;

        [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")]
        [Range(1, 50)]
        public int smoothing = 5;
        [Range(0, 10)]
        [Tooltip("Delay between iterations")]
        public float delay = 5;
        [Tooltip("Duration of iterations")]
        [MinMaxRange(0, 10)]
        public RangedFloat duration = new RangedFloat(5, 5);

        [Separator("Audio")]
        public List<LightFlickerAudio> flickerAudios;
    }
}