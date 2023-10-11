using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace xNodes.Nodes.Delay
{
    [CreateNodeMenu("Actions/Delay/Fixed Delay")]
    public class DelayNode : BaseDelayNode
    {
        [SerializeField] private float duration;

        public override void Execute()
        {
            StaticCoroutine.Start(WaitForTime(duration));
        }

        public override void RunDelayFunction(Action delayFinishedCallback)
        {
            StaticCoroutine.Start(WaitForTime(duration));
        }

        protected override IEnumerator WaitForTime(float waitTime, Action callback = null)
        {
            yield return !useTimeScale ? new WaitForSecondsRealtime(waitTime) : new WaitForSeconds(waitTime);
            callback?.Invoke();

            NextNode("exit");
        }
    }
}