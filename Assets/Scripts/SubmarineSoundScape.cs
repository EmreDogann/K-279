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

    /// <summary>
    ///     Will trigger audio playback and trigger shake if enabled.
    /// </summary>
    /// <param name="playbackPosition">The world position at which to play the audio.</param>
    /// <param name="impulseListener">The impulse listener to receive the shake.</param>
    /// <param name="triggerImpulseOverride">The shake mode override. Default is ShakeOverride.DefaultShake.</param>
    /// <param name="randomizePosition">Whether or not to randomize the playback and shake position.</param>
    /// <param name="minRadius">The minimum radius for randomization.</param>
    /// <param name="maxRadius">The maximum radius for randomization.</param>
    /// <returns>The strength of the shake (0 means no shake).</returns>
    public float TriggerPlayback(Vector3 playbackPosition, CinemachineImpulseListener impulseListener,
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
            Vector3 randomPoint = Random.onUnitSphere * (maxRadius - Random.Range(0.0f, maxRadius - minRadius));
            // Debug.Log($"Random Point: {randomPoint.magnitude}");
            position += randomPoint;
        }

        audio.Stop(true);
        audio.Play(position);

        switch (triggerImpulseOverride)
        {
            case ShakeOverride.NoShake:
                break;
            case ShakeOverride.ForceShake:
                return SetupAndShake(playbackPosition, position, impulseListener);
            case ShakeOverride.DefaultShake:
                if (triggerImpulse && shakeProfile && impulseSource)
                {
                    return SetupAndShake(playbackPosition, position, impulseListener);
                }

                break;
        }

        // No shake.
        return 0.0f;
    }

    private float SetupAndShake(Vector3 playerPosition, Vector3 finalPlaybackPosition,
        CinemachineImpulseListener impulseListener)
    {
        _impulseDefinition.m_ImpulseDuration = shakeProfile.impactTime;
        impulseSource.m_DefaultVelocity = shakeProfile.defaultVelocity;
        impulseListener.m_ReactionSettings.m_AmplitudeGain = shakeProfile.listenerAmplitude;
        impulseListener.m_ReactionSettings.m_FrequencyGain = shakeProfile.listenerFrequency;
        impulseListener.m_ReactionSettings.m_Duration = shakeProfile.listenerDuration;

        Vector3 velocity = impulseSource.transform.position - finalPlaybackPosition;
        // Make velocity inversely proportional.
        float newMagnitude = Mathf.Pow(1.3f,
            (playRadius + (impulseSource.transform.position - playerPosition).magnitude -
             velocity.magnitude) / 7.0f);
        velocity = velocity.normalized * newMagnitude;

        impulseSource.GenerateImpulseAtPositionWithVelocity(finalPlaybackPosition,
            velocity * shakeProfile.impactForce);

        return newMagnitude;
    }
}

public class SubmarineSoundScape : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseListener impulseListener;
    [SerializeField] private List<AmbientAudioPlayback> ambientAudios;

    private readonly Dictionary<SoundType, AmbientAudioPlayback> _soundTypeToAudio =
        new Dictionary<SoundType, AmbientAudioPlayback>();

    private GameObject _player;

    public static event Action<float> ExplosionTriggered;

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

    private IEnumerator ForcePlaySoundScape(AmbientAudioPlayback ambientAudio, float delay, Vector3 playbackPosition)
    {
        if (delay > 0.0f)
        {
            yield return new WaitForSecondsRealtime(delay);
        }

        float shakeStrength = ambientAudio.TriggerPlayback(playbackPosition, impulseListener);
        if (ambientAudio.soundType == SoundType.Explosion && shakeStrength > 0.0f)
        {
            ExplosionTriggered?.Invoke(shakeStrength);
        }
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
                float shakeStrength = ambientAudio.TriggerPlayback(_player.transform.position, impulseListener);
                if (ambientAudio.soundType == SoundType.Explosion && shakeStrength > 0.0f)
                {
                    ExplosionTriggered?.Invoke(shakeStrength);
                }

                return true;
            }
        }

        return false;
    }

    [ButtonMethod]
    public void TriggerSqueeze()
    {
        _soundTypeToAudio[SoundType.Squeeze].TriggerPlayback(_player.transform.position, impulseListener);
    }

    [ButtonMethod]
    public void TriggerSonar()
    {
        _soundTypeToAudio[SoundType.Sonar].TriggerPlayback(_player.transform.position, impulseListener);
    }

    [ButtonMethod]
    private void TriggerExplosion()
    {
        float shakeStrength = _soundTypeToAudio[SoundType.Explosion]
            .TriggerPlayback(_player.transform.position, impulseListener);
        if (shakeStrength > 0.0f)
        {
            ExplosionTriggered?.Invoke(shakeStrength);
        }

        if (_soundTypeToAudio[SoundType.Explosion].IsImpulseEnabled() && Random.Range(0.0f, 1.0f) <= 0.5f)
        {
            // StartCoroutine(ForcePlaySoundScape(_soundTypeToAudio[SoundType.Squeeze], Random.Range(0.0f, 0.5f),
            //     _player.transform.position));
        }
    }

    public void TriggerSound(SoundType soundType, ShakeOverride shakeOverride = ShakeOverride.DefaultShake,
        float minPlayRadius = -1.0f,
        float maxPlayRadius = -1.0f)
    {
        float shakeStrength = _soundTypeToAudio[soundType].TriggerPlayback(_player.transform.position, impulseListener,
            shakeOverride, true,
            minPlayRadius,
            maxPlayRadius);
        if (_soundTypeToAudio[soundType].soundType == SoundType.Explosion && shakeStrength > 0.0f)
        {
            ExplosionTriggered?.Invoke(shakeStrength);
        }
    }
}