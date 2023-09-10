using System;
using AYellowpaper;
using UnityEngine;

namespace ScriptedEvents
{
    public class SequencePlayer : MonoBehaviour
    {
        [SerializeField] private InterfaceReference<IEventTrigger, MonoBehaviour> eventTrigger;
        [SerializeField] private InterfaceReference<IScriptedSequence, MonoBehaviour> scriptedSequence;

        private void Start()
        {
            if (eventTrigger.Value == null)
            {
                eventTrigger = new InterfaceReference<IEventTrigger, MonoBehaviour>(GetComponent<IEventTrigger>());
            }

            if (eventTrigger.Value != null)
            {
                eventTrigger.Value.EventTriggered += PlaySequence;
            }
        }

        private void Reset()
        {
            eventTrigger = new InterfaceReference<IEventTrigger, MonoBehaviour>(GetComponent<IEventTrigger>());
        }

        private void PlaySequence(object sender, EventArgs e)
        {
            scriptedSequence.Value.PlaySequence();
        }
    }
}