using System;
using System.Collections;
using UnityEngine;
using Utils;
using xNodes.Nodes.Delay;

namespace xNodes.Nodes
{
    [NodeWidth(400)]
    [CreateNodeMenu("Actions/Animation/Trigger Animation")]
    public class TriggerAnimationNode : BaseNode
    {
        [Serializable]
        public class AnimFinishedCallback {}

        [SerializeField] private Animator _animator;
        [SerializeField] private string animationName;
        [SerializeField] private string triggerName;
        [SerializeField] private bool waitForAnimationFinish;
        [Output] [SerializeField] private AnimFinishedCallback animFinishedEvent;

        public override void Execute()
        {
            _animator.SetTrigger(triggerName);
            StaticCoroutine.Start(WaitForAnimationFinished(_animator, animationName));
            if (!waitForAnimationFinish)
            {
                NextNode("exit");
            }
        }

        private IEnumerator WaitForAnimationFinished(Animator animator, string animationName)
        {
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) ||
                   animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null;
            }

            TriggerOutput(nameof(animFinishedEvent));
            if (waitForAnimationFinish)
            {
                NextNode("exit");
            }
        }
    }
}