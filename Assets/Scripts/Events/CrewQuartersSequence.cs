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

public class CrewQuartersSequence : MonoBehaviour {
    // Move the player into position, play Ship explosion noise, play alarm, play low oxygen voice, fade to normal.
    private Sequence _bottleRollingSequence;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private SubmarineSoundScape submarineSoundScape;

    [SerializeField]
    private AudioSO squeezeSound;
    private float squeezeSoundVolumeOverride = 0.2f;

    [SerializeField]
    private AudioSO bottleRollSound;
    [SerializeField]
    private Transform bottleRollSoundTransformStart;
    // [SerializeField]
    // private Transform bottleRollSoundTransformEnd;

    private Vector3 bottlePosition;


    [SerializeField]
    private AudioSO doorLockSound;
    [SerializeField]
    private Transform doorLockTransform;

    [SerializeField]
    private AudioSO lowOxygenVoiceSound;

    [SerializeField]
    private LightManager lightManager;

#if UNITY_EDITOR
    [ButtonMethod]
    public void PlayCrewQuartersSequenceButton() {
        // _bottleRollingSequence.Play();
        PlayCrewQuartersSequence();
    }
#endif
    public void PlayCrewQuartersSequence() {
        _bottleRollingSequence.Restart();
        // _bottleRollingSequence.Play();
    }
    void Start() {
        _bottleRollingSequence = DOTween.Sequence();

        _bottleRollingSequence
            .AppendCallback(() => {
                // Play ship explosion noise
                submarineSoundScape.TriggerExplosion(true);
            })
            .AppendInterval(0.1f)
            .AppendCallback(() => {
                // Play ship explosion noise
                // submarineSoundScape.TriggerExplosion(true);
                // squeezeSound.Play(playerTransform.position, volumeOverride: squeezeSoundVolumeOverride);
                bottleRollSound.Play(bottleRollSoundTransformStart.position);
            })
            // .AppendInterval(0.05f)
            // .AppendCallback(() => {
            //     // Play ship explosion noise
            //     submarineSoundScape.TriggerExplosion(true);
            // })
            .AppendInterval(1.5f)
            .AppendCallback(() => {
                // play Low Oxygen Voice
                doorLockSound.Play(doorLockTransform.position, volumeOverride:0.1f);
            })
            // .AppendInterval(doorOpenSound.GetPlaybackInfo(AudioHandle.Invalid).CurrentClipDuration)
            .AppendInterval(2.0f)
            .AppendCallback(() => {
                lowOxygenVoiceSound.Play(playerTransform.position);
                // Fade to normal lights
                // LightControl.OnLightControl?.Invoke(true, 1.0f);
            })
            .Pause();
    }
}