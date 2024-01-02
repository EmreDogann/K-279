using System;
using System.Collections;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace xNode.Nodes.Delay
{
    [CreateNodeMenu("Actions/Delay/Random Delay")]
    public class RandomDelayNode : BaseDelayNode
    {
        [SerializeField] private float minTime;
        [SerializeField] private float maxTime;

        public override void Execute()
        {
            StaticCoroutine.Run(WaitForTime(Random.Range(minTime, maxTime)));
        }

        public override void RunDelayFunction(Action delayFinishedCallback)
        {
            StaticCoroutine.Run(WaitForTime(Random.Range(minTime, maxTime), delayFinishedCallback));
        }

        protected override IEnumerator WaitForTime(float waitTime, Action callback = null)
        {
            yield return useTimeScale ? new WaitForSecondsRealtime(waitTime) : new WaitForSeconds(waitTime);
            callback?.Invoke();

            NextNode("exit");
        }
    }
}