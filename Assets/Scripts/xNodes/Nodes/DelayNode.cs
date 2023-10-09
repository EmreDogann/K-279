using System.Collections;
using UnityEngine;
using Utils;

namespace xNodes.Nodes
{
    [CreateNodeMenu("Actions/Delay")]
    public class DelayNode : BaseNode
    {
        [SerializeField] private float duration;

        public override void Execute()
        {
            StaticCoroutine.Start(WaitForTime(duration));
        }

        private IEnumerator WaitForTime(float waitTime)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            NextNode("exit");
        }
    }
}