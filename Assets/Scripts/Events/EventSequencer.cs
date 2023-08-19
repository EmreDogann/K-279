using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using DG.Tweening;
using Lights;
using MyBox;
using Rooms;
using UnityEngine;
using UnityEngine.Serialization;

public class EventSequencer : MonoBehaviour {
    // Move the player into position, play Ship explosion noise, play alarm, play low oxygen voice, fade to normal.
    private Sequence _wakeUpSequence;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private SubmarineSoundScape submarineSoundScape;

    [SerializeField]
    private AudioSO alarmSound;

    [SerializeField]
    private AudioSO lowOxygenVoice;

#if UNITY_EDITOR
    [ButtonMethod]
    public void PlayWakeUpSequence() {
        _wakeUpSequence.Play();
    }
#endif

    private void Awake() {
    }

    // Start is called before the first frame update
    void Start() {
        _wakeUpSequence = DOTween.Sequence();


        _wakeUpSequence
            .AppendCallback(() => { LightControl.OnLightControl?.Invoke(false, 0.0f); })
            .AppendCallback(() => {
                var playerTransformPosition = playerTransform.position;
                playerTransformPosition.x = 22;
                playerTransform.position = playerTransformPosition;
            })
            .AppendCallback(() => {
                // Play ship explosion noise
                submarineSoundScape.TriggerExplosion();
                alarmSound.Play();
            })
            .AppendCallback(() => {
                // play Low Oxygen Voice
                lowOxygenVoice.Play();
            })
            // .AppendInterval(lowOxygenVoice.GetPlaybackInfo(AudioHandle.Invalid).CurrentClipDuration)
            .AppendInterval(lowOxygenVoice.GetAudioClip().length)
            .AppendCallback(() => {
                // Fade to normal lights
                LightControl.OnLightControl?.Invoke(true, 1.0f);
            }).Pause();
    }

    // Update is called once per frame
    void Update() {
    }
}