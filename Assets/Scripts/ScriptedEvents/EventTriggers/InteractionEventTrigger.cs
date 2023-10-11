using System;
using Interactables;
using UnityEngine;

namespace ScriptedEvents.EventTriggers
{
    public class InteractionEventTrigger : MonoBehaviour, IEventTrigger, IInteractableObjects
    {
        [SerializeField] private bool singleUse;

        private bool _isTriggered;

        public event EventHandler EventTriggered;

        public bool IsTriggered()
        {
            return _isTriggered;
        }

        public void InteractionStart()
        {
            if (!singleUse && _isTriggered)
            {
                _isTriggered = false;
            }

            if (!_isTriggered)
            {
                OnEventTriggered();
            }

            _isTriggered = true;
        }

        public void InteractionContinues() {}
        public void InteractionEnd() {}

        public void InteractionAreaEnter() {}
        public void InteractionAreaExit() {}

        public bool IsInteractable()
        {
            return !_isTriggered;
        }

        private void OnEventTriggered()
        {
            EventTriggered?.Invoke(this, EventArgs.Empty);
        }
    }
}