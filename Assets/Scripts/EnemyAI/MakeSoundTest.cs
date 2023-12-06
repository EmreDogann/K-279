using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EnemyAI
{
    public class MakeSoundTest : MonoBehaviour, IDragHandler
    {
        [SerializeField] private AudioSO audioToPlay;
        [SerializeField] private float soundRange = 25.0f;
        [SerializeField] private AudioAlertType soundAlertType = AudioAlertType.Dangerous;

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("clicked");
            audioToPlay.Play();
            AudioAlert.Trigger(soundAlertType, transform);
        }

    }
}

