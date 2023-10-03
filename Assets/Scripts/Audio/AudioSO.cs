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

        private List<KeyValuePair<AudioHandle, AudioEventData>> _handleToEventData;
        private List<KeyValuePair<AudioHandle, AudioEventData>> HandleToEventData
        {
            get
            {
                if (_handleToEventData == null)
                {
                    _handleToEventData = new List<KeyValuePair<AudioHandle, AudioEventData>>();
                }

                return _handleToEventData;
            }
        }

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
            // _handleToEventData = new List<KeyValuePair<AudioHandle, AudioEventData>>();
        }

        protected override void OnEnd()
        {
            _handleToEventData = null;
        }

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

            // _handleToEventData = new List<KeyValuePair<AudioHandle, AudioEventData>>();
            // _handleToEventData = null;
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

        private int FindHandleIndex(AudioHandle handle)
        {
            int index = 0;
            foreach (var entry in HandleToEventData)
            {
                if (entry.Key == handle)
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        private void OnHandleStale(AudioHandle handle)
        {
            handle.OnHandleStale -= OnHandleStale;
            HandleToEventData.RemoveAt(FindHandleIndex(handle));
        }

        public AudioHandle Play(Vector3 positionWorldSpace = default, bool fadeIn = false,
            float fadeDuration = 1.0f, float volumeOverride = -1)
        {
            if (clips.Length == 0)
            {
                Debug.LogWarning($"No sound clips for {name}");
                return AudioHandle.Invalid;
            }

            AudioEventData audioEventData = new AudioEventData();
            audioEventData.Volume = volumeOverride >= 0.0f ? volumeOverride : Random.Range(volume.Min, volume.Max);
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
                HandleToEventData.Add(new KeyValuePair<AudioHandle, AudioEventData>(handle, audioEventData));
                handle.OnHandleStale += OnHandleStale;
                return handle;
            }

            return AudioHandle.Invalid;
        }

        public AudioHandle Play2D(bool fadeIn = false, float fadeDuration = 1.0f, float volumeOverride = -1)
        {
            if (clips.Length == 0)
            {
                Debug.LogWarning($"No sound clips for {name}");
                return AudioHandle.Invalid;
            }

            AudioEventData audioEventData = new AudioEventData();
            audioEventData.Volume = volumeOverride >= 0.0f ? volumeOverride : Random.Range(volume.Min, volume.Max);
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
                HandleToEventData.Add(new KeyValuePair<AudioHandle, AudioEventData>(handle, audioEventData));
                handle.OnHandleStale += OnHandleStale;
                return handle;
            }

            return AudioHandle.Invalid;
        }

        public AudioHandle PlayAttached(GameObject gameObject, bool fadeIn = false,
            float fadeDuration = 1.0f, float volumeOverride = -1)
        {
            if (clips.Length == 0)
            {
                Debug.LogWarning($"No sound clips for {name}");
                return AudioHandle.Invalid;
            }

            AudioEventData audioEventData = new AudioEventData();
            audioEventData.Volume = volumeOverride >= 0.0f ? volumeOverride : Random.Range(volume.Min, volume.Max);
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
                HandleToEventData.Add(new KeyValuePair<AudioHandle, AudioEventData>(handle, audioEventData));
                handle.OnHandleStale += OnHandleStale;
                return handle;
            }

            return AudioHandle.Invalid;
        }

        /// <summary>
        ///     Stop all currently playing audio instances
        /// </summary>
        /// <param name="fadeOut">Should audio fade out when stopping?</param>
        /// <param name="fadeDuration">Length of audio fade out in seconds.</param>
        public void StopAll(bool fadeOut = false, float fadeDuration = 1.0f)
        {
            if (HandleToEventData.Count == 0)
            {
                return;
            }

            var dataCopy = HandleToEventData.GetRange(0, HandleToEventData.Count);
            foreach (var entry in dataCopy)
            {
                Stop(entry.Key, fadeOut, fadeDuration);
            }

            // HandleToEventData.Clear();
        }

        /// <summary>
        ///     Stops the most recently playing audio instance if exists.
        /// </summary>
        /// <param name="fadeOut">Should audio fade out when stopping?</param>
        /// <param name="fadeDuration">Length of audio fade out in seconds.</param>
        public void Stop(bool fadeOut = false, float fadeDuration = 1.0f)
        {
            Stop(AudioHandle.Invalid, fadeOut, fadeDuration);
        }

        /// <summary>
        ///     Stop a specific audio instance via the provided audio handle.
        /// </summary>
        /// <param name="audioHandle">The handle of the currently playing audio instance.</param>
        /// <param name="fadeOut">Should audio fade out when stopping?</param>
        /// <param name="fadeDuration">Length of audio fade out in seconds.</param>
        public void Stop(AudioHandle audioHandle, bool fadeOut = false, float fadeDuration = 1.0f)
        {
            if (HandleToEventData.Count == 0)
            {
                return;
            }

            int index = audioHandle == AudioHandle.Invalid
                ? HandleToEventData.Count - 1
                : FindHandleIndex(audioHandle);
            if (index == -1)
            {
                return;
            }

            AudioHandle handle = HandleToEventData[index].Key;
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
        }

        public void FadeAudioAll(float to, float duration)
        {
            foreach (var entry in HandleToEventData)
            {
                FadeAudio(entry.Key, to, duration);
            }
        }

        public void FadeAudio(AudioHandle audioHandle, float to, float duration)
        {
            if (HandleToEventData.Count < 1 || FindHandleIndex(audioHandle) == -1)
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
            foreach (var entry in HandleToEventData)
            {
                UnFadeAudio(entry.Key, duration);
            }
        }

        public void UnFadeAudio(AudioHandle audioHandle, float duration)
        {
            int index = FindHandleIndex(audioHandle);
            if (HandleToEventData.Count < 1 || index == -1)
            {
                return;
            }

            bool handleFound = audioEvent.RaiseFadeEvent(audioHandle, HandleToEventData[index].Value.Volume, duration);

            if (!handleFound)
            {
                Debug.LogWarning($"Audio {audioHandle.Audio.name} could not be unfaded. Handle is stale.");
            }
        }

        public void CrossFadeAudio(AudioSO transitionAudio, float duration)
        {
            audioEvent.RaiseCrossFadeEvent(this, transitionAudio, duration);
        }

        public AudioStateInfo GetPlaybackInfo(AudioHandle audioHandle)
        {
            if (HandleToEventData.Count < 1)
            {
                return null;
            }

            int index = audioHandle == AudioHandle.Invalid
                ? HandleToEventData.Count - 1
                : FindHandleIndex(audioHandle);
            if (index == -1)
            {
                return null;
            }

            AudioHandle handle = audioHandle == AudioHandle.Invalid ? HandleToEventData[^1].Key : audioHandle;
            AudioStateInfo stateInfo = audioEvent.RaiseGetStateEvent(handle);

            return stateInfo;
        }
    }
}