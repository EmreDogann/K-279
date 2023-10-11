using System;
using AYellowpaper;
using ScriptedEvents;
using UnityEngine;
using xNodes.Nodes.Delay;

namespace xNodes.Nodes
{
    [NodeWidth(400)]
    [CreateNodeMenu("Actions/Wait For Trigger")]
    public class WaitForTriggerNode : BaseNode
    {
        [SerializeField] private InterfaceReference<IEventTrigger, MonoBehaviour> eventTrigger;
        public Transform playerTransform;

        protected override void Init()
        {
            if (eventTrigger == null)
            {
                eventTrigger = new InterfaceReference<IEventTrigger, MonoBehaviour>();
            }

            if (eventTrigger.Value != null)
            {
                eventTrigger.Value.EventTriggered += OnEventTrigger;
            }
        }

        public override void Execute()
        {
            if (eventTrigger.Value == null)
            {
                Debug.LogError("Trigger Node " + name + " is not assigned an event trigger!");
            }
        }

        private void OnDestroy()
        {
            if (eventTrigger.Value != null)
            {
                eventTrigger.Value.EventTriggered -= OnEventTrigger;
            }
        }

        private void OnEventTrigger(object sender, EventArgs e)
        {
            NextNode("exit");
        }
    }
}