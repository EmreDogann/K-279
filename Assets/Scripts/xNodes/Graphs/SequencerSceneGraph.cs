using System;
using AYellowpaper;
using MyBox;
using ScriptedEvents;
using UnityEngine;
using XNode;

namespace xNodes.Graphs
{
    public class SequencerSceneGraph : SceneGraph<SequencerGraph>
    {
        [SerializeField] private bool triggerOnStart;
        [OverrideLabel("Start when Event Triggered")] [ConditionalField(nameof(triggerOnStart), true)]
        [RequireInterface(typeof(IEventTrigger))]
        [SerializeField] private MonoBehaviour eventTrigger;

        private void Start()
        {
            if (triggerOnStart)
            {
                graph.Start();
            }

            if (eventTrigger != null)
            {
                ((IEventTrigger)eventTrigger).EventTriggered += OnEventTrigger;
            }
        }

        private void OnDestroy()
        {
            if (eventTrigger != null)
            {
                ((IEventTrigger)eventTrigger).EventTriggered -= OnEventTrigger;
            }
        }

        private void OnEventTrigger(object sender, EventArgs e)
        {
            graph.Start();
        }
    }
}