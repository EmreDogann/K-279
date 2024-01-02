using System;
using System.Collections;
using UnityEngine;

namespace xNode.Nodes.Delay
{
    public abstract class BaseDelayNode : BaseNode
    {
        [Tooltip("Delay will continue even when game is paused using Time.timescale = 0")]
        [Output] [SerializeField] private BaseDelayNode delayFunction;
        [SerializeField] protected bool useTimeScale = true;

        /// <summary>
        ///     Called when used as a delay function.
        /// </summary>
        /// <param name="delayFinishedCallback">The callback function to invoke when the delay is finished.</param>
        public abstract void RunDelayFunction(Action delayFinishedCallback);

        protected abstract IEnumerator WaitForTime(float waitTime, Action callback = null);
    }
}