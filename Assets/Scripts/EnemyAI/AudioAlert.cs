using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public static class AudioAlert
    {
        public static event Action<AudioAlertType, Transform> OnTriggerAudioAlert = delegate { };
        public static void Trigger(AudioAlertType alertType, Transform playerTransform)
        {
            OnTriggerAudioAlert?.Invoke(alertType, playerTransform);
        }
    }
}

