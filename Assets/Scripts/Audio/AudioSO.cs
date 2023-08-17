using System.Collections.Generic;
using Events;
using MyBox;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [CreateAssetMenu(fileName = "New Audio", menuName = "Audio/New Audio")]
    public class AudioSO : ManagedScriptableObject
    {
        private enum SoundClipPlayOrder
        {
            Random,
            InOrder,
            Reverse
        }

        private const float SEMITONES_TO_PITCH_CONVERSION_UNIT = 1.05946f;

        [MustBeAssigned] [SerializeField] private AudioClip[] clips;

        [Separator("Clip Settings")]
        [MustBeAssigned] [SerializeField] private AudioMixerGroup audioMixer;
        [MinMaxRange(0, 1)] public RangedFloat volume = new RangedFloat(0.5f, 0.5f);

        public bool useSemitones;

        [ConditionalField(nameof(useSemitones))]
        [MinMaxRange(-10, 10)] [SerializeField] private RangedInt semitones = new RangedInt(0, 0);

        [ConditionalField(nameof(useSemitones), true)]
        [MinMaxRange(0, 3)] [SerializeField] private RangedFloat pitch = new RangedFloat(1.0f, 1.0f);

        [Tooltip("Should the audio loop until specified to stop?")]
        [SerializeField] private bool Looping;

        [Tooltip("Should the audio allow being paused when the game is paused.")]
        [SerializeField] private bool CanBePaused;

        [Separator("Playback Order")]
        [SerializeField] private SoundClipPlayOrder playOrder;

        [ReadOnly] [SerializeField] private int currentPlayIndex;

        [Separator("Audio Events")]
        [Tooltip("The Audio Event to trigger when trying to play/stop the audio.")]
        [OverrideLabel("Play Trigger Event")] [SerializeField] private AudioEventChannelSO audioEvent;
        private List<AudioHandle> _audioHandle;
        private Dictionary<AudioHandle, AudioEventData> _audioHandleData;

        #region PreviewCode

#if UNITY_EDITOR
        private AudioSource previewer;

        private void OnValidate()
        {
            SyncPitchAndSemitones();
        }


        [ButtonMethod]
        public void PlayPreview()
        {
            PlayPreview(previewer);
        }

        [ButtonMethod]
        private void StopPreview()
        {
            previewer.Stop();
        }

        private void PlayPreview(AudioSource audioSource)
        {
            if (clips.Length == 0)
            {
                Debug.LogError($"No sound clips for {name}");
                return;
            }

            if (audioMixer == null)
            {
                Debug.LogError("No mixer provided, aborting...");
                return;
            }

            AudioSource source = audioSource;
            if (source == null)
            {
                Debug.LogError("No audio source provided, aborting...");
                return;
            }

            source.outputAudioMixerGroup = audioMixer;
            source.clip = GetAudioClip();
            source.volume = Random.Range(volume.Min, volume.Max);
            source.pitch = useSemitones
                ? Mathf.Pow(SEMITONES_TO_PITCH_CONVERSION_UNIT, Random.Range(semitones.Min, semitones.Max))
                : Random.Range(pitch.Min, pitch.Max);

            source.Play();
        }
#endif

        #endregion

        protected override void OnBegin()
        {
            _audioHandle = new List<AudioHandle>();
            _audioHandleData = new Dictionary<AudioHandle, AudioEventData>();
        }

        protected override void OnEnd() {}

        protected override void OnEnable()
        {
            base.OnEnable();
#if UNITY_EDITOR
            previewer = EditorUtility
                .CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave,
                    typeof(AudioSource))
                .GetComponent<AudioSource>();
#endif
        }

        protected override void OnDisable()
        {
            base.OnDisable();
#if UNITY_EDITOR
            DestroyImmediate(previewer.gameObject);
#endif

            _audioHandle = new List<AudioHandle>();
        }

        private void SyncPitchAndSemitones()
        {
            if (useSemitones)
            {
                pitch.Min = Mathf.Pow(SEMITONES_TO_PITCH_CONVERSION_UNIT, semitones.Min);
                pitch.Max = Mathf.Pow(SEMITONES_TO_PITCH_CONVERSION_UNIT, semitones.Max);
            }
            else
            {
                semitones.Min =
                    Mathf.RoundToInt(Mathf.Log10(pitch.Min) / Mathf.Log10(SEMITONES_TO_PITCH_CONVERSION_UNIT));
                semitones.Max =
                    Mathf.RoundToInt(Mathf.Log10(pitch.Max) / Mathf.Log10(SEMITONES_TO_PITCH_CONVERSION_UNIT));
            }
        }

        public AudioClip GetAudioClip()
        {
            // Get current clip
            AudioClip clip = clips[currentPlayIndex >= clips.Length ? 0 : currentPlayIndex];

            // Find next clip
            switch (playOrder)
            {
                case SoundClipPlayOrder.InOrder:
                    currentPlayIndex = (currentPlayIndex + 1) % clips.Length;
                    break;
                case SoundClipPlayOrder.Random:
                    currentPlayIndex = Random.Range(0, clips.Length);
                    break;
                case SoundClipPlayOrder.Reverse:
                    currentPlayIndex = (currentPlayIndex + clips.Length - 1) % clips.Length;
                    break;
            }

            return clip;
        }

        public AudioMixerGroup GetAudioMixer()
        {
            return audioMixer;
        }

        public AudioHandle Play(Vector3 positionWorldSpace = default, bool fadeIn = false, float fadeDuration = 1.0f)
        {
            if (clips.Length == 0)
            {
                Debug.LogWarning($"No sound clips for {name}");
                return AudioHandle.Invalid;
            }

            AudioEventData audioEventData = new AudioEventData();
            audioEventData.Volume = Random.Range(volume.Min, volume.Max);
            audioEventData.Pitch = useSemitones
                ? Mathf.Pow(SEMITONES_TO_PITCH_CONVERSION_UNIT, Random.Range(semitones.Min, semitones.Max))
                : Random.Range(pitch.Min, pitch.Max);
            audioEventData.ShouldLoop = Looping;
            audioEventData.CanPause = CanBePaused;
            if (fadeIn)
            {
                audioEventData.SoundFade = new SoundFade
                {
                    FadeType = FadeType.FadeIn,
                    Volume = audioEventData.Volume,
                    Duration = fadeDuration
                };
            }

            AudioHandle handle = audioEvent.RaisePlayEvent(this, audioEventData, positionWorldSpace);
            if (handle != AudioHandle.Invalid)
            {
                _audioHandle.Add(handle);
                _audioHandleData.TryAdd(handle, audioEventData);
                return handle;
            }

            return AudioHandle.Invalid;
        }

        public AudioHandle Play2D(bool fadeIn = false, float fadeDuration = 1.0f)
        {
            if (clips.Length == 0)
            {
                Debug.LogWarning($"No sound clips for {name}");
                return AudioHandle.Invalid;
            }

            AudioEventData audioEventData = new AudioEventData();
            audioEventData.Volume = Random.Range(volume.Min, volume.Max);
            audioEventData.Pitch = useSemitones
                ? Mathf.Pow(SEMITONES_TO_PITCH_CONVERSION_UNIT, Random.Range(semitones.Min, semitones.Max))
                : Random.Range(pitch.Min, pitch.Max);
            audioEventData.ShouldLoop = Looping;
            audioEventData.CanPause = CanBePaused;
            if (fadeIn)
            {
                audioEventData.SoundFade = new SoundFade
                {
                    FadeType = FadeType.FadeIn,
                    Volume = audioEventData.Volume,
                    Duration = fadeDuration
                };
            }

            AudioHandle handle = audioEvent.RaisePlay2DEvent(this, audioEventData);
            if (handle != AudioHandle.Invalid)
            {
                _audioHandle.Add(handle);
                _audioHandleData.TryAdd(handle, audioEventData);
                return handle;
            }

            return AudioHandle.Invalid;
        }

        public AudioHandle PlayAttached(GameObject gameObject, bool fadeIn = false, float fadeDuration = 1.0f)
        {
            if (clips.Length == 0)
            {
                Debug.LogWarning($"No sound clips for {name}");
                return AudioHandle.Invalid;
            }

            AudioEventData audioEventData = new AudioEventData();
            audioEventData.Volume = Random.Range(volume.Min, volume.Max);
            audioEventData.Pitch = useSemitones
                ? Mathf.Pow(SEMITONES_TO_PITCH_CONVERSION_UNIT, Random.Range(semitones.Min, semitones.Max))
                : Random.Range(pitch.Min, pitch.Max);
            audioEventData.ShouldLoop = Looping;
            audioEventData.CanPause = CanBePaused;
            if (fadeIn)
            {
                audioEventData.SoundFade = new SoundFade
                {
                    FadeType = FadeType.FadeIn,
                    Volume = audioEventData.Volume,
                    Duration = fadeDuration
                };
            }

            AudioHandle handle = audioEvent.RaisePlayAttachedEvent(this, audioEventData, gameObject);
            if (handle != AudioHandle.Invalid)
            {
                _audioHandle.Add(handle);
                _audioHandleData.TryAdd(handle, audioEventData);
                return handle;
            }

            return AudioHandle.Invalid;
        }

        public void StopAll()
        {
            if (_audioHandle.Count < 1)
            {
                audioEvent.RaiseStopEvent(AudioHandle.Invalid, null);
                return;
            }

            foreach (AudioHandle handle in _audioHandle)
            {
                bool handleFound = audioEvent.RaiseStopEvent(handle, null);

                if (!handleFound)
                {
                    Debug.LogWarning($"Audio {handle.Audio.name} could not be stopped. Handle is stale.");
                }
            }

            _audioHandle.Clear();
            _audioHandleData.Clear();
        }

        public void Stop(bool fadeOut = false, float fadeDuration = 1.0f)
        {
            Stop(AudioHandle.Invalid, fadeOut, fadeDuration);
        }

        public void Stop(AudioHandle audioHandle, bool fadeOut = false, float fadeDuration = 1.0f)
        {
            if (_audioHandle.Count < 1)
            {
                audioEvent.RaiseStopEvent(AudioHandle.Invalid, null);
                return;
            }

            AudioHandle handle = audioHandle == AudioHandle.Invalid ? _audioHandle[^1] : audioHandle;
            SoundFade soundFade = null;
            if (fadeOut)
            {
                soundFade = new SoundFade
                {
                    FadeType = FadeType.FadeOut,
                    Volume = 0.0f,
                    Duration = fadeDuration
                };
            }

            bool handleFound = audioEvent.RaiseStopEvent(handle, soundFade);

            if (!handleFound)
            {
                Debug.LogWarning($"Audio {handle.Audio.name} could not be stopped. Handle is stale.");
            }

            _audioHandle.Remove(handle);
            _audioHandleData.Remove(handle);
        }

        public void FadeAudioAll(float to, float duration)
        {
            foreach (AudioHandle handle in _audioHandle)
            {
                FadeAudio(handle, to, duration);
            }
        }

        public void FadeAudio(AudioHandle audioHandle, float to, float duration)
        {
            if (_audioHandle.Count < 1 || !_audioHandle.Contains(audioHandle))
            {
                return;
            }

            bool handleFound = audioEvent.RaiseFadeEvent(audioHandle, to, duration);

            if (!handleFound)
            {
                Debug.LogWarning($"Audio {audioHandle.Audio.name} could not be faded. Handle is stale.");
            }
        }

        public void UnFadeAudioAll(float duration)
        {
            foreach (AudioHandle handle in _audioHandle)
            {
                UnFadeAudio(handle, duration);
            }
        }

        public void UnFadeAudio(AudioHandle audioHandle, float duration)
        {
            if (_audioHandle.Count < 1 || !_audioHandle.Contains(audioHandle))
            {
                return;
            }

            bool handleFound = audioEvent.RaiseFadeEvent(audioHandle, _audioHandleData[audioHandle].Volume, duration);

            if (!handleFound)
            {
                Debug.LogWarning($"Audio {audioHandle.Audio.name} could not be unfaded. Handle is stale.");
            }
        }

        public void CrossFadeAudio(AudioSO transitionAudio, float duration)
        {
            audioEvent.RaiseCrossFadeEvent(this, transitionAudio, duration);
        }
    }
}