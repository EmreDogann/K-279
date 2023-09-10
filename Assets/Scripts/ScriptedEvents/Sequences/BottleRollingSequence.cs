using Audio;
using DG.Tweening;
using MyBox;
using UnityEngine;

namespace ScriptedEvents.Sequences
{
    public class BottleRollingSequence : MonoBehaviour, IScriptedSequence
    {
        [Separator("Transforms")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform doorLockTransform;
        [SerializeField] private Transform bottleRollSoundTransformStart;

        [Separator("Audio")]
        [SerializeField] private SubmarineSoundScape submarineSoundScape;
        [SerializeField] private Animator bottleAnimator;

        [SerializeField] private AudioSO bottleRollSound;
        [SerializeField] private AudioSO doorLockSound;
        [SerializeField] private AudioSO lowOxygenVoiceSound;

        private Sequence _bottleRollingSequence;
        private Vector3 _bottlePosition;
        private static readonly int RollBottle = Animator.StringToHash("RollBottle");

#if UNITY_EDITOR
        [ButtonMethod]
        public void PlayCrewQuartersSequenceButton()
        {
            PlayCrewQuartersSequence();
        }
#endif
        public void PlayCrewQuartersSequence()
        {
            _bottleRollingSequence.Restart();
        }

        private void Start()
        {
            _bottleRollingSequence = DOTween.Sequence();

            _bottleRollingSequence
                .AppendCallback(() =>
                {
                    // Play ship explosion noise
                    submarineSoundScape.TriggerSound(SoundType.Explosion, ShakeOverride.ForceShake, 0.1f, 1.0f);
                })
                .AppendInterval(0.5f)
                .AppendCallback(() =>
                {
                    bottleAnimator.SetTrigger(RollBottle);
                    bottleRollSound.Play(bottleRollSoundTransformStart.position);
                })
                // .AppendInterval(0.05f)
                // .AppendCallback(() => {
                //     // Play ship explosion noise
                //     submarineSoundScape.TriggerExplosion(true);
                // })
                .AppendInterval(1.5f)
                .AppendCallback(() =>
                {
                    // play Low Oxygen Voice
                    doorLockSound.Play(doorLockTransform.position, volumeOverride: 0.1f);
                })
                // .AppendInterval(doorOpenSound.GetPlaybackInfo(AudioHandle.Invalid).CurrentClipDuration)
                .AppendInterval(2.0f)
                .AppendCallback(() =>
                {
                    lowOxygenVoiceSound.Play(playerTransform.position);
                    // Fade to normal lights
                    // LightControl.OnLightControl?.Invoke(true, 1.0f);
                })
                .SetAutoKill(false)
                .Pause();
        }

        public void PlaySequence()
        {
            PlayCrewQuartersSequence();
        }
    }
}