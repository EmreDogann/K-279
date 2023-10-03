using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using DG.Tweening;
using MyBox;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScriptedEvents.Sequences
{
    public class BottleRollingSequence : MonoBehaviour, IScriptedSequence
    {
        [Separator("Transforms")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform bottleTransform;
        [SerializeField] private List<Transform> bedTransforms;

        [Separator("Animations")]
        [SerializeField] private Animator bottleAnimator;

        [Separator("Audio")]
        [SerializeField] private SubmarineSoundScape submarineSoundScape;

        [SerializeField] private AudioSO bottleRollSound;
        [SerializeField] private AudioSO lowOxygenVoiceSound;

        [SerializeField] private AudioSO bedSpringsSound;

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
                    foreach (Transform bedTransform in bedTransforms)
                    {
                        StartCoroutine(RandomDelay(() => { bedSpringsSound.Play(bedTransform.position); }));
                    }
                })
                .AppendInterval(0.25f)
                .AppendCallback(() =>
                {
                    bottleAnimator.SetTrigger(RollBottle);
                    StartCoroutine(WaitForAnimationFinished(bottleAnimator, "BottleRolling"));
                })
                .AppendInterval(0.1f)
                .AppendCallback(() => { bottleRollSound.PlayAttached(bottleTransform.gameObject); })
                .AppendInterval(5.0f)
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

        private IEnumerator RandomDelay(Action delayFinishedCallback)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            delayFinishedCallback?.Invoke();
        }
    }
}