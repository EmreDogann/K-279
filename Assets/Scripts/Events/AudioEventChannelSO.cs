using Audio;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "New Audio Event", menuName = "Game Event/Audio Event", order = 4)]
    public class AudioEventChannelSO : ScriptableObject
    {
        public AudioPlayAction OnAudioPlay;
        public AudioPlay2DAction OnAudioPlay2D;
        public AudioPlayAttachedAction OnAudioPlayAttached;
        public AudioStopAction OnAudioStop;
        public AudioFadeAction OnAudioFade;
        public AudioCrossFadeAction OnAudioCrossFade;

        public AudioHandle RaisePlayEvent(AudioSO audio, AudioEventData audioEventData,
            Vector3 positionInSpace = default)
        {
            AudioHandle audioHandle = AudioHandle.Invalid;

            if (OnAudioPlay != null)
            {
                audioHandle = OnAudioPlay.Invoke(audio, audioEventData, positionInSpace);
            }
            else
            {
                Debug.LogWarning("An AudioPlay event was requested for " + audio.name +
                                 ", but nobody picked it up. " +
                                 "Check why there is no AudioManager already loaded, " +
                                 "and make sure it's listening on this Audio Event channel.");
            }

            return audioHandle;
        }

        public AudioHandle RaisePlay2DEvent(AudioSO audio, AudioEventData audioEventData)
        {
            AudioHandle audioHandle = AudioHandle.Invalid;

            if (OnAudioPlay != null)
            {
                audioHandle = OnAudioPlay2D.Invoke(audio, audioEventData);
            }
            else
            {
                Debug.LogWarning("An AudioPlay2D event was requested for " + audio.name +
                                 ", but nobody picked it up. " +
                                 "Check why there is no AudioManager already loaded, " +
                                 "and make sure it's listening on this Audio Event channel.");
            }

            return audioHandle;
        }

        public AudioHandle RaisePlayAttachedEvent(AudioSO audio, AudioEventData audioEventData,
            GameObject gameObject)
        {
            AudioHandle audioHandle = AudioHandle.Invalid;

            if (OnAudioPlay != null)
            {
                audioHandle = OnAudioPlayAttached.Invoke(audio, audioEventData, gameObject);
            }
            else
            {
                Debug.LogWarning("An AudioPlayAttached event was requested for " + audio.name +
                                 ", but nobody picked it up. " +
                                 "Check why there is no AudioManager already loaded, " +
                                 "and make sure it's listening on this Audio Event channel.");
            }

            return audioHandle;
        }

        public bool RaiseStopEvent(AudioHandle audioKey, SoundFade soundFade)
        {
            bool requestSucceed = false;

            if (OnAudioStop != null)
            {
                requestSucceed = OnAudioStop.Invoke(audioKey, soundFade);
            }
            else
            {
                Debug.LogWarning("An AudioStop event was requested, but nobody picked it up. " +
                                 "Check why there is no AudioManager already loaded, " +
                                 "and make sure it's listening on this Audio Event channel.");
            }

            return requestSucceed;
        }

        public bool RaiseFadeEvent(AudioHandle audioKey, float to, float duration)
        {
            bool requestSucceed = false;

            if (OnAudioStop != null)
            {
                requestSucceed = OnAudioFade.Invoke(audioKey, to, duration);
            }
            else
            {
                Debug.LogWarning("An AudioFade event was requested, but nobody picked it up. " +
                                 "Check why there is no AudioManager already loaded, " +
                                 "and make sure it's listening on this Audio Event channel.");
            }

            return requestSucceed;
        }

        public bool RaiseCrossFadeEvent(AudioSO audio, AudioSO transitionAudio, float duration)
        {
            bool requestSucceed = false;

            if (OnAudioStop != null)
            {
                requestSucceed = OnAudioCrossFade.Invoke(audio, transitionAudio, duration);
            }
            else
            {
                Debug.LogWarning("An AudioCrossFade event was requested, but nobody picked it up. " +
                                 "Check why there is no AudioManager already loaded, " +
                                 "and make sure it's listening on this Audio Event channel.");
            }

            return requestSucceed;
        }
    }

    public delegate AudioHandle AudioPlayAction(AudioSO audio, AudioEventData audioEventData,
        Vector3 positionInSpace);

    public delegate AudioHandle AudioPlay2DAction(AudioSO audio, AudioEventData audioEventData);

    public delegate AudioHandle AudioPlayAttachedAction(AudioSO audio, AudioEventData audioEventData,
        GameObject gameObject);

    public delegate bool AudioStopAction(AudioHandle emitterKey, SoundFade soundFade);

    public delegate bool AudioFadeAction(AudioHandle emitterKey, float to, float duration);

    public delegate bool AudioCrossFadeAction(AudioSO audio, AudioSO transitionAudio, float duration);
}