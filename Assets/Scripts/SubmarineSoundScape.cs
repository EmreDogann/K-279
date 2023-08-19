using System;
using System.Collections;
using Audio;
using Cinemachine;
using MyBox;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class AmbientAudioPlayback
{
    public bool enabled = true;
    public AudioSO audio;
    public float playChance = 0.5f;
    public float playCooldown = 3.0f;
    [HideInInspector] public float timer;

    [SerializeField] private float playRadius = 10.0f;
    [SerializeField] private float minPlayRadius = 5.0f;
    [SerializeField] private bool triggerImpulse;
    [ConditionalField(nameof(triggerImpulse))] public ScreenShakeProfile shakeProfile;
    [ConditionalField(nameof(triggerImpulse))] public CinemachineImpulseSource impulseSource;
    private CinemachineImpulseDefinition _impulseDefinition;

    public void Setup(CinemachineImpulseListener impulseListener)
    {
        if (triggerImpulse && shakeProfile && impulseSource)
        {
            _impulseDefinition = impulseSource.m_ImpulseDefinition;
        }
    }

    public void TriggerAudio(Vector3 playbackPosition, CinemachineImpulseListener impulseListener,
        bool randomizePosition = true)
    {
        Vector3 position = playbackPosition;
        if (randomizePosition)
        {
            Vector3 randomPoint = Random.onUnitSphere * playRadius;
            randomPoint -= Vector3.one * Random.Range(0.0f, minPlayRadius);
            position += randomPoint;
        }

        audio.Stop(true);
        audio.Play(position);

        if (triggerImpulse && shakeProfile && impulseSource)
        {
            _impulseDefinition.m_ImpulseDuration = shakeProfile.impactTime;
            impulseSource.m_DefaultVelocity = shakeProfile.defaultVelocity;
            impulseListener.m_ReactionSettings.m_AmplitudeGain = shakeProfile.listenerAmplitude;
            impulseListener.m_ReactionSettings.m_FrequencyGain = shakeProfile.listenerFrequency;
            impulseListener.m_ReactionSettings.m_Duration = shakeProfile.listenerDuration;

            impulseSource.GenerateImpulseAtPositionWithVelocity(position,
                (impulseSource.transform.position - position).normalized * shakeProfile.impactForce);
        }
    }
}

public class SubmarineSoundScape : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseListener impulseListener;

    [SerializeField] private AmbientAudioPlayback submarineSqueeze;
    [SerializeField] private AmbientAudioPlayback sonar;
    [SerializeField] private AmbientAudioPlayback explosion;

    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");

        submarineSqueeze.Setup(impulseListener);
        sonar.Setup(impulseListener);
        explosion.Setup(impulseListener);

        CinemachineImpulseManager.Instance.IgnoreTimeScale = true;
    }

    private void OnValidate()
    {
        submarineSqueeze.Setup(impulseListener);
        sonar.Setup(impulseListener);
        explosion.Setup(impulseListener);
    }

    private void Update()
    {
        submarineSqueeze.timer += Time.unscaledDeltaTime;
        sonar.timer += Time.unscaledDeltaTime;
        explosion.timer += Time.unscaledDeltaTime;

        TryPlaySoundScape(sonar);
        TryPlaySoundScape(submarineSqueeze);
        if (TryPlaySoundScape(explosion))
        {
            if (Random.Range(0.0f, 1.0f) <= 0.5f)
            {
                StartCoroutine(ForcePlaySoundScape(submarineSqueeze, Random.Range(0.0f, 0.5f),
                    _player.transform.position));
            }
        }
    }

    private IEnumerator ForcePlaySoundScape(AmbientAudioPlayback ambientPlayback, float delay, Vector3 playbackPosition)
    {
        if (delay > 0.0f)
        {
            yield return new WaitForSecondsRealtime(delay);
        }

        ambientPlayback.TriggerAudio(playbackPosition, impulseListener);
    }

    private bool TryPlaySoundScape(AmbientAudioPlayback ambientAudio)
    {
        if (!ambientAudio.enabled)
        {
            return false;
        }

        if (ambientAudio.timer >= ambientAudio.playCooldown)
        {
            ambientAudio.timer = 0.0f;
            AudioStateInfo playbackInfo = ambientAudio.audio.GetPlaybackInfo(AudioHandle.Invalid);

            if (playbackInfo != null && (playbackInfo.IsPlaying || playbackInfo.IsPaused))
            {
                return false;
            }

            if (Random.Range(0.0f, 1.0f) <= ambientAudio.playChance)
            {
                ambientAudio.TriggerAudio(_player.transform.position, impulseListener);

                return true;
            }
        }

        return false;
    }

    [ButtonMethod]
    public void TriggerSqueeze(bool shouldShake)
    {
        submarineSqueeze.TriggerAudio(_player.transform.position, impulseListener);
    }

    [ButtonMethod]
    public void TriggerSonar(bool shouldShake)
    {
        sonar.TriggerAudio(_player.transform.position, impulseListener);
    }

    [ButtonMethod]
    private void TriggerExplosionWithShake()
    {
        TriggerExplosion(true);
    }

    public void TriggerExplosion(bool shouldShake)
    {
        explosion.TriggerAudio(_player.transform.position, impulseListener);
        if (shouldShake && Random.Range(0.0f, 1.0f) <= 0.5f)
        {
            StartCoroutine(ForcePlaySoundScape(submarineSqueeze, Random.Range(0.0f, 0.5f), _player.transform.position));
        }
    }
}