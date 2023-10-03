using System.Collections;
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
        [SerializeField] private Transform bottleTransform;

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
                .AppendInterval(0.25f)
                .AppendCallback(() =>
                {
                    bottleAnimator.SetTrigger(RollBottle);
                    StartCoroutine(WaitForAnimationFinished(bottleAnimator, "BottleRolling"));
                })
                .AppendInterval(0.1f)
                .AppendCallback(() => { bottleRollSound.PlayAttached(bottleTransform.gameObject); })
                .AppendInterval(1.5f)
                .AppendCallback(() =>
                {
                    // play Low Oxygen Voice
                    doorLockSound.Play(doorLockTransform.position, volumeOverride: 0.1f);
                })
                .AppendInterval(2.0f)
                .AppendCallback(() => { lowOxygenVoiceSound.Play(playerTransform.position); })
                .SetAutoKill(false)
                .Pause();
        }

        public void PlaySequence()
        {
            PlayCrewQuartersSequence();
        }

        private IEnumerator WaitForAnimationFinished(Animator animator, string animationName)
        {
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) ||
                   animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null;
            }

            bottleRollSound.Stop(true);
        }
    }
}