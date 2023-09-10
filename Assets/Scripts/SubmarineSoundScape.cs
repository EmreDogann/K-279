using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Cinemachine;
using MyBox;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
///     NoShake - Override default setting and force no shake.
///     ForceShake - Override default setting and force shake.
///     DefaultShake - Use whatever shake setting was set at design time.
/// </summary>
public enum ShakeOverride
{
    NoShake,
    ForceShake,
    DefaultShake
}

public enum SoundType
{
    Explosion,
    Squeeze,
    Sonar
}

[Serializable]
public class AmbientAudioPlayback
{
    public bool enabled = true;
    public SoundType soundType;
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

    public bool IsImpulseEnabled()
    {
        return triggerImpulse;
    }

    public void TriggerAudio(Vector3 playbackPosition, CinemachineImpulseListener impulseListener,
        ShakeOverride triggerImpulseOverride = ShakeOverride.DefaultShake, bool randomizePosition = true,
        float minRadius = -1.0f,
        float maxRadius = -1.0f)
    {
        if (minRadius < 0.0f)
        {
            minRadius = minPlayRadius;
        }

        if (maxRadius < 0.0f)
        {
            maxRadius = playRadius;
        }

        Vector3 position = playbackPosition;
        if (randomizePosition)
        {
            Vector3 randomPoint = Random.onUnitSphere * maxRadius;
            randomPoint -= Vector3.one * Random.Range(0.0f, minRadius);
            position += randomPoint;
        }

        audio.Stop(true);
        audio.Play(position);

        switch (triggerImpulseOverride)
        {
            case ShakeOverride.NoShake:
                break;
            case ShakeOverride.ForceShake:
                SetupAndShake(position, impulseListener);
                break;
            case ShakeOverride.DefaultShake:
                if (triggerImpulse && shakeProfile && impulseSource)
                {
                    SetupAndShake(position, impulseListener);
                }

                break;
        }
    }

    private void SetupAndShake(Vector3 playbackPosition, CinemachineImpulseListener impulseListener)
    {
        _impulseDefinition.m_ImpulseDuration = shakeProfile.impactTime;
        impulseSource.m_DefaultVelocity = shakeProfile.defaultVelocity;
        impulseListener.m_ReactionSettings.m_AmplitudeGain = shakeProfile.listenerAmplitude;
        impulseListener.m_ReactionSettings.m_FrequencyGain = shakeProfile.listenerFrequency;
        impulseListener.m_ReactionSettings.m_Duration = shakeProfile.listenerDuration;

        impulseSource.GenerateImpulseAtPositionWithVelocity(playbackPosition,
            (impulseSource.transform.position - playbackPosition).normalized * shakeProfile.impactForce);
    }
}

public class SubmarineSoundScape : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseListener impulseListener;
    [SerializeField] private List<AmbientAudioPlayback> ambientAudios;

    private readonly Dictionary<SoundType, AmbientAudioPlayback> _soundTypeToAudio =
        new Dictionary<SoundType, AmbientAudioPlayback>();

    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");

        _soundTypeToAudio.Clear();
        foreach (AmbientAudioPlayback ambientAudio in ambientAudios)
        {
            ambientAudio.Setup(impulseListener);
            _soundTypeToAudio[ambientAudio.soundType] = ambientAudio;
        }

        CinemachineImpulseManager.Instance.IgnoreTimeScale = true;
    }

    private void OnValidate()
    {
        _soundTypeToAudio.Clear();
        foreach (AmbientAudioPlayback ambientAudio in ambientAudios)
        {
            ambientAudio.Setup(impulseListener);
            _soundTypeToAudio[ambientAudio.soundType] = ambientAudio;
        }
    }

    private void Update()
    {
        foreach (AmbientAudioPlayback ambientAudio in ambientAudios)
        {
            ambientAudio.timer += Time.unscaledDeltaTime;

            if (TryPlaySoundScape(ambientAudio) && ambientAudio.soundType == SoundType.Explosion)
            {
                if (Random.Range(0.0f, 1.0f) <= 0.5f)
                {
                    StartCoroutine(ForcePlaySoundScape(_soundTypeToAudio[SoundType.Squeeze], Random.Range(0.0f, 0.5f),
                        _player.transform.position));
                }
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
    public void TriggerSqueeze()
    {
        _soundTypeToAudio[SoundType.Squeeze].TriggerAudio(_player.transform.position, impulseListener);
    }

    [ButtonMethod]
    public void TriggerSonar()
    {
        _soundTypeToAudio[SoundType.Sonar].TriggerAudio(_player.transform.position, impulseListener);
    }

    [ButtonMethod]
    private void TriggerExplosion()
    {
        _soundTypeToAudio[SoundType.Explosion]
            .TriggerAudio(_player.transform.position, impulseListener);

        if (_soundTypeToAudio[SoundType.Explosion].IsImpulseEnabled() && Random.Range(0.0f, 1.0f) <= 0.5f)
        {
            StartCoroutine(ForcePlaySoundScape(_soundTypeToAudio[SoundType.Squeeze], Random.Range(0.0f, 0.5f),
                _player.transform.position));
        }
    }

    public void TriggerSound(SoundType soundType, ShakeOverride shakeOverride = ShakeOverride.DefaultShake,
        float minPlayRadius = -1.0f,
        float maxPlayRadius = -1.0f)
    {
        foreach (AmbientAudioPlayback ambientAudio in ambientAudios)
        {
            if (ambientAudio.soundType != soundType)
            {
                continue;
            }

            ambientAudio.TriggerAudio(_player.transform.position, impulseListener, shakeOverride, true, minPlayRadius,
                maxPlayRadius);
        }
    }
}