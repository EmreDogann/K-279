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

public class WakeUpSequence : MonoBehaviour {
    // Move the player into position, play Ship explosion noise, play alarm, play low oxygen voice, fade to normal.
    private Sequence _wakeUpSequence;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private SubmarineSoundScape submarineSoundScape;

    [SerializeField]
    private AudioSO explosionSound;
    
    [SerializeField]
    private AudioSO lowOxygenVoice;

    [SerializeField]
    private LightManager lightManager;

#if UNITY_EDITOR
    [ButtonMethod]
    public void PlayWakeUpSequence() {
        _wakeUpSequence.Play();
    }
#endif

    void Start() {
        _wakeUpSequence = DOTween.Sequence();
        
        _wakeUpSequence
            .AppendCallback(() => {
                LightControl.OnLightControl?.Invoke(false, 0.0f);
            })
            .AppendCallback(() => {
                var playerTransformPosition = playerTransform.position;
                playerTransformPosition.x = 22;
                playerTransform.position = playerTransformPosition;
            })
            .AppendCallback(() => {
                // Play ship explosion noise
                submarineSoundScape.TriggerExplosion(true);
            })
            .AppendInterval(0.1f)
            .AppendCallback(() => {
                // Play ship explosion noise
                // submarineSoundScape.TriggerExplosion(true);
                explosionSound.Play(playerTransform.position);
            })
            .AppendInterval(0.05f)
            .AppendCallback(() => {
                // Play ship explosion noise
                submarineSoundScape.TriggerExplosion(true);
            })
            .AppendInterval(1.5f)
            .AppendCallback(() => {
                // play Low Oxygen Voice
                lowOxygenVoice.Play();
            })
            // .AppendInterval(lowOxygenVoice.GetPlaybackInfo(AudioHandle.Invalid).CurrentClipDuration)
            .AppendInterval(lowOxygenVoice.GetAudioClip().length)
            .AppendCallback(() => {
                lightManager.SetLightState(LightState.Alarm);
                // Fade to normal lights
                // LightControl.OnLightControl?.Invoke(true, 1.0f);
            }).Pause();
    }

}