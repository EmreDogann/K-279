using System.Collections.Generic;
using DG.Tweening;
using Events;
using MyBox;
using UnityEngine;

namespace Audio
{
    public class AudioEmitter
    {
        public bool CanPause;
        public bool IsPaused;
        public float DefaultSpatialBlend;
        public AudioSource Source;
        public GameObject AttachedGameObject;
    }

    public class AudioManager : MonoBehaviour
    {
        [Separator("Pooling")]
        [SerializeField] private int audioSourcePoolSize;
        [SerializeField] private GameObject audioSourcePrefab;

        [Separator("Event Channels")]
        [SerializeField] private AudioEventChannelSO sfxAudioChannel;
        [SerializeField] private AudioEventChannelSO musicAudioChannel;
        [SerializeField] private BoolEventListener onPauseEvent;
        [SerializeField] private BoolEventListener onGameEndEvent;

        private List<AudioEmitter> _audioEmitters;
        private List<AudioHandle> _audioHandles;
        private AudioEmitter _musicEmitter;
        private AudioSource _musicEmitterCrossFade;
        private AudioSource _musicEmitterTransition;

        private int _currentAudioSourceIndex;

        private void Awake()
        {
            if (audioSourcePrefab.GetComponent<AudioSource>() == null)
            {
                Debug.LogError(
                    "ERROR (Audio Manager -> Awake()): Provided audio source prefab contains no audio source component! Aborting audio manager initialization...");
                return;
            }

            _audioEmitters = new List<AudioEmitter>();
            _audioHandles = new List<AudioHandle>();
            _musicEmitter = null;
            _musicEmitterCrossFade = Instantiate(audioSourcePrefab, transform).GetComponent<AudioSource>();
            _musicEmitterTransition = Instantiate(audioSourcePrefab, transform).GetComponent<AudioSource>();

            for (int i = 0; i < audioSourcePoolSize; i++)
            {
                AudioEmitter emitter = CreateAudioEmitter();
                _audioEmitters.Add(emitter);
            }
        }

        private void OnEnable()
        {
            onPauseEvent.Response.AddListener(OnPauseEvent);
            onGameEndEvent.Response.AddListener(OnGameEnd);

            sfxAudioChannel.OnAudioPlay += PlaySoundEffect;
            sfxAudioChannel.OnAudioPlay2D += PlaySoundEffect2D;
            sfxAudioChannel.OnAudioPlayAttached += PlaySoundEffectAttached;
            sfxAudioChannel.OnAudioStop += StopSoundEffect;
            sfxAudioChannel.OnAudioFade += FadeSoundEffect;

            musicAudioChannel.OnAudioPlay += PlayMusic;
            musicAudioChannel.OnAudioPlay2D += PlayMusic2D;
            musicAudioChannel.OnAudioStop += StopMusic;
            musicAudioChannel.OnAudioFade += FadeMusic;
            musicAudioChannel.OnAudioCrossFade += CrossFadeMusic;
        }

        private void OnDestroy()
        {
            onPauseEvent.Response.RemoveListener(OnPauseEvent);
            onGameEndEvent.Response.RemoveListener(OnGameEnd);

            sfxAudioChannel.OnAudioPlay -= PlaySoundEffect;
            sfxAudioChannel.OnAudioPlay2D -= PlaySoundEffect2D;
            sfxAudioChannel.OnAudioPlayAttached -= PlaySoundEffectAttached;
            sfxAudioChannel.OnAudioStop -= StopSoundEffect;
            sfxAudioChannel.OnAudioFade -= FadeSoundEffect;

            musicAudioChannel.OnAudioPlay -= PlayMusic;
            musicAudioChannel.OnAudioPlay2D -= PlayMusic2D;
            musicAudioChannel.OnAudioStop -= StopMusic;
            musicAudioChannel.OnAudioFade -= FadeMusic;
            musicAudioChannel.OnAudioCrossFade -= CrossFadeMusic;
        }

        private void Update()
        {
            foreach (AudioEmitter emitter in _audioEmitters)
            {
                if (emitter.AttachedGameObject)
                {
                    emitter.Source.transform.position = emitter.AttachedGameObject.transform.position;
                }
            }
        }

        private void OnGameEnd(bool didWin)
        {
            for (int i = _audioHandles.Count - 1; i >= 0; i--)
            {
                StopSoundEffect(_audioHandles[i], new SoundFade
                {
                    Duration = 3.0f,
                    FadeType = FadeType.FadeOut,
                    Volume = 0.0f
                });
            }

            StopMusic(AudioHandle.Invalid, new SoundFade
            {
                Duration = 3.0f,
                FadeType = FadeType.FadeOut,
                Volume = 0.0f
            });
        }

        public AudioHandle PlaySoundEffect2D(AudioSO audioObj, AudioEventData audioEventData)
        {
            AudioEmitter emitter = RequestAudioEmitter();
            emitter.CanPause = audioEventData.CanPause;
            emitter.AttachedGameObject = null;

            emitter.Source.outputAudioMixerGroup = audioObj.GetAudioMixer();
            emitter.Source.transform.position = Vector3.zero;
            emitter.Source.clip = audioObj.GetAudioClip();
            emitter.Source.volume = audioEventData.Volume;
            emitter.Source.pitch = audioEventData.Pitch;
            emitter.Source.spatialBlend = 0;
            emitter.Source.loop = audioEventData.ShouldLoop;
            //Reset in case this AudioSource is being reused for a short SFX after being used for a long music track
            emitter.Source.time = 0f;

            if (audioEventData.SoundFade != null)
            {
                emitter.Source.volume = 0.0f;
                emitter.Source.Play();
                emitter.Source.DOFade(audioEventData.SoundFade.Volume, audioEventData.SoundFade.Duration);
            }
            else
            {
                emitter.Source.Play();
            }

            AudioHandle handle = new AudioHandle(_currentAudioSourceIndex, audioObj);
            _audioHandles.Add(handle);

            return handle;
        }

        public AudioHandle PlaySoundEffect(AudioSO audioObj, AudioEventData audioEventData, Vector3 positionInSpace)
        {
            AudioEmitter emitter = RequestAudioEmitter();
            emitter.CanPause = audioEventData.CanPause;
            emitter.AttachedGameObject = null;

            emitter.Source.outputAudioMixerGroup = audioObj.GetAudioMixer();
            emitter.Source.transform.position = positionInSpace;
            emitter.Source.clip = audioObj.GetAudioClip();
            emitter.Source.volume = audioEventData.Volume;
            emitter.Source.pitch = audioEventData.Pitch;
            emitter.Source.spatialBlend = emitter.DefaultSpatialBlend;
            emitter.Source.loop = audioEventData.ShouldLoop;
            //Reset in case this AudioSource is being reused for a short SFX after being used for a long music track
            emitter.Source.time = 0f;

            if (audioEventData.SoundFade != null)
            {
                emitter.Source.volume = 0.0f;
                emitter.Source.Play();
                emitter.Source.DOFade(audioEventData.SoundFade.Volume, audioEventData.SoundFade.Duration);
            }
            else
            {
                emitter.Source.Play();
            }

            AudioHandle handle = new AudioHandle(_currentAudioSourceIndex, audioObj);
            _audioHandles.Add(handle);

            return handle;
        }

        public AudioHandle PlaySoundEffectAttached(AudioSO audioObj, AudioEventData audioEventData, GameObject gameObj)
        {
            AudioEmitter emitter = RequestAudioEmitter();
            emitter.CanPause = audioEventData.CanPause;
            emitter.AttachedGameObject = gameObj;

            emitter.Source.outputAudioMixerGroup = audioObj.GetAudioMixer();
            emitter.Source.transform.position = gameObj.transform.position;
            emitter.Source.clip = audioObj.GetAudioClip();
            emitter.Source.volume = audioEventData.Volume;
            emitter.Source.pitch = audioEventData.Pitch;
            emitter.Source.loop = audioEventData.ShouldLoop;
            //Reset in case this AudioSource is being reused for a short SFX after being used for a long music track
            emitter.Source.time = 0f;

            if (audioEventData.SoundFade != null)
            {
                emitter.Source.volume = 0.0f;
                emitter.Source.Play();
                emitter.Source.DOFade(audioEventData.SoundFade.Volume, audioEventData.SoundFade.Duration);
            }
            else
            {
                emitter.Source.Play();
            }

            AudioHandle handle = new AudioHandle(_currentAudioSourceIndex, audioObj);
            _audioHandles.Add(handle);

            return handle;
        }

        public AudioHandle PlayMusic2D(AudioSO audioObj, AudioEventData audioEventData)
        {
            if (_musicEmitter != null && _musicEmitter.Source.isPlaying)
            {
                // Maybe can do fancy things here like fade out audio instead of a hard stop.
                _musicEmitter.Source.Stop();
            }

            AudioEmitter emitter = RequestAudioEmitter();
            emitter.CanPause = audioEventData.CanPause;
            emitter.AttachedGameObject = null;

            emitter.Source.outputAudioMixerGroup = audioObj.GetAudioMixer();
            emitter.Source.transform.position = Vector3.zero;
            emitter.Source.clip = audioObj.GetAudioClip();
            emitter.Source.volume = audioEventData.Volume;
            emitter.Source.pitch = audioEventData.Pitch;
            emitter.Source.spatialBlend = 0;
            emitter.Source.loop = audioEventData.ShouldLoop;
            //Reset in case this AudioSource is being reused for a short SFX after being used for a long music track
            emitter.Source.time = 0f;

            if (audioEventData.SoundFade != null)
            {
                emitter.Source.volume = 0.0f;
                emitter.Source.Play();
                emitter.Source.DOFade(audioEventData.SoundFade.Volume, audioEventData.SoundFade.Duration);
            }
            else
            {
                emitter.Source.Play();
            }

            _musicEmitter = emitter;
            return new AudioHandle(_currentAudioSourceIndex, audioObj);
        }

        public AudioHandle PlayMusic(AudioSO audioObj, AudioEventData audioEventData, Vector3 positionInSpace)
        {
            if (_musicEmitter != null && _musicEmitter.Source.isPlaying)
            {
                // Maybe can do fancy things here like fade out audio instead of a hard stop.
                _musicEmitter.Source.Stop();
            }

            AudioEmitter emitter = RequestAudioEmitter();
            emitter.CanPause = audioEventData.CanPause;
            emitter.AttachedGameObject = null;

            emitter.Source.outputAudioMixerGroup = audioObj.GetAudioMixer();
            emitter.Source.transform.position = positionInSpace;
            emitter.Source.clip = audioObj.GetAudioClip();
            emitter.Source.volume = audioEventData.Volume;
            emitter.Source.pitch = audioEventData.Pitch;
            emitter.Source.spatialBlend = emitter.DefaultSpatialBlend;
            emitter.Source.loop = audioEventData.ShouldLoop;
            //Reset in case this AudioSource is being reused for a short SFX after being used for a long music track
            emitter.Source.time = 0f;

            if (audioEventData.SoundFade != null)
            {
                emitter.Source.volume = 0.0f;
                emitter.Source.Play();
                emitter.Source.DOFade(audioEventData.SoundFade.Volume, audioEventData.SoundFade.Duration);
            }
            else
            {
                emitter.Source.Play();
            }

            _musicEmitter = emitter;
            return AudioHandle.Invalid;
        }

        public bool StopSoundEffect(AudioHandle handle, SoundFade soundFade)
        {
            int handleIndex = _audioHandles.FindIndex(x => x == handle);

            if (handleIndex < 0)
            {
                return false;
            }

            AudioHandle foundHandle = _audioHandles[handleIndex];

            if (soundFade != null)
            {
                _audioEmitters[foundHandle.ID].Source
                    .DOFade(0.0f, soundFade.Duration)
                    .SetUpdate(true)
                    .OnComplete(() => { _audioEmitters[foundHandle.ID].Source.Stop(); });
            }
            else
            {
                _audioEmitters[foundHandle.ID].Source.Stop();
            }

            _audioEmitters[foundHandle.ID].IsPaused = false;

            _audioHandles.RemoveAt(handleIndex);
            return true;
        }

        public bool StopMusic(AudioHandle handle, SoundFade soundFade)
        {
            if (_musicEmitter != null && _musicEmitter.Source.isPlaying)
            {
                if (soundFade != null)
                {
                    _musicEmitter.Source
                        .DOFade(0.0f, soundFade.Duration)
                        .SetUpdate(true)
                        .OnComplete(() => { _musicEmitter.Source.Stop(); });
                }
                else
                {
                    _musicEmitter.Source.Stop();
                }

                return true;
            }

            return false;
        }

        private bool FadeSoundEffect(AudioHandle handle, float to, float duration)
        {
            int handleIndex = _audioHandles.FindIndex(x => x == handle);

            if (handleIndex < 0)
            {
                return false;
            }

            AudioHandle foundHandle = _audioHandles[handleIndex];

            _audioEmitters[foundHandle.ID].Source.DOKill();
            _audioEmitters[foundHandle.ID].Source.DOFade(to, duration);
            return true;
        }

        private bool FadeMusic(AudioHandle handle, float to, float duration)
        {
            if (_musicEmitter != null && _musicEmitter.Source.isPlaying)
            {
                _musicEmitter.Source.DOKill();
                _musicEmitter.Source.DOFade(to, duration).SetUpdate(true);
                return true;
            }

            return false;
        }

        private bool CrossFadeMusic(AudioSO audio, AudioSO transitionAudio, float duration)
        {
            if (_musicEmitter == null || !_musicEmitter.Source.isPlaying)
            {
                return false;
            }

            _musicEmitterCrossFade.Stop();
            _musicEmitterTransition.Stop();

            _musicEmitterCrossFade.outputAudioMixerGroup = _musicEmitter.Source.outputAudioMixerGroup;
            _musicEmitterCrossFade.transform.position = _musicEmitter.Source.transform.position;
            _musicEmitterCrossFade.clip = _musicEmitter.Source.clip;
            _musicEmitterCrossFade.volume = _musicEmitter.Source.volume;
            _musicEmitterCrossFade.pitch = _musicEmitter.Source.pitch;
            _musicEmitterCrossFade.spatialBlend = _musicEmitter.Source.spatialBlend;
            _musicEmitterCrossFade.loop = _musicEmitter.Source.loop;
            _musicEmitterCrossFade.time = _musicEmitter.Source.time;

            _musicEmitterTransition.outputAudioMixerGroup = _musicEmitter.Source.outputAudioMixerGroup;
            _musicEmitterTransition.transform.position = _musicEmitter.Source.transform.position;
            _musicEmitterTransition.clip = transitionAudio.GetAudioClip();
            _musicEmitterTransition.volume = Random.Range(transitionAudio.volume.Min, transitionAudio.volume.Max);
            _musicEmitterTransition.pitch = _musicEmitter.Source.pitch;
            _musicEmitterTransition.spatialBlend = _musicEmitter.Source.spatialBlend;
            _musicEmitterTransition.loop = _musicEmitter.Source.loop;
            _musicEmitterTransition.time = 0f;

            float prevVolume = _musicEmitter.Source.volume;
            // _musicEmitter.Source.Stop();
            _musicEmitter.Source.clip = audio.GetAudioClip();
            _musicEmitter.Source.volume = 0.0f;
            // _musicEmitter.Source.time = 0.0f;
            _musicEmitter.Source.Play();
            _musicEmitter.Source.DOFade(prevVolume, duration).OnComplete(() =>
            {
                _musicEmitterTransition.DOFade(0.0f, 2.0f).OnComplete(
                    () => { _musicEmitterTransition.Stop(); });
            });

            _musicEmitterCrossFade.Play();
            _musicEmitterCrossFade.DOFade(0.0f, duration / 2.0f).OnComplete(() => { _musicEmitterCrossFade.Stop(); });

            _musicEmitterTransition.Play();
            return true;
        }

        private void OnPauseEvent(bool isPaused)
        {
            foreach (AudioEmitter emitter in _audioEmitters)
            {
                if (isPaused)
                {
                    if (emitter.CanPause && emitter.Source.isPlaying)
                    {
                        emitter.Source.Pause();
                        emitter.IsPaused = true;
                    }
                }
                else
                {
                    if (emitter.IsPaused)
                    {
                        emitter.Source.Play();
                        emitter.IsPaused = false;
                    }
                }
            }

            if (_musicEmitter != null && _musicEmitter.CanPause && _musicEmitter.Source.isPlaying)
            {
                if (isPaused)
                {
                    _musicEmitter.Source.Pause();
                }
                else
                {
                    _musicEmitter.Source.Play();
                }

                _musicEmitter.IsPaused = isPaused;
            }
        }

        private AudioEmitter RequestAudioEmitter()
        {
            int emitterIndex = TryGetAvailableEmitter();

            if (emitterIndex < 0)
            {
                AudioEmitter emitter = CreateAudioEmitter();
                _audioEmitters.Add(emitter);

                _currentAudioSourceIndex = _audioEmitters.Count - 1;
                return emitter;
            }

            return _audioEmitters[emitterIndex];
        }

        private int TryGetAvailableEmitter()
        {
            AudioEmitter emitter = _audioEmitters[_currentAudioSourceIndex];
            if (!emitter.IsPaused && !emitter.Source.isPlaying)
            {
                return _currentAudioSourceIndex;
            }

            for (int i = 0; i < _audioEmitters.Count; i++)
            {
                if (!_audioEmitters[i].IsPaused && !_audioEmitters[i].Source.isPlaying)
                {
                    _currentAudioSourceIndex = i;
                    return _currentAudioSourceIndex;
                }
            }

            return -1;
        }

        private AudioEmitter CreateAudioEmitter()
        {
            AudioEmitter emitter = new AudioEmitter();
            emitter.Source = Instantiate(audioSourcePrefab, transform).GetComponent<AudioSource>();
            emitter.DefaultSpatialBlend = emitter.Source.spatialBlend;
            emitter.CanPause = false;
            emitter.IsPaused = false;

            return emitter;
        }
    }
}