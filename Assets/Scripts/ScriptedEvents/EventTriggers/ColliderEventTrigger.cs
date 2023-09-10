using System;
using UnityEngine;
using Utils;

namespace ScriptedEvents.EventTriggers
{
    public class ColliderEventTrigger : MonoBehaviour, IEventTrigger
    {
        [SerializeField] private LayerMask collisionLayerMask = ~0;
        [SerializeField] private bool singleUse;

        private bool _isTriggered;

        public event EventHandler EventTriggered;

        public bool IsTriggered()
        {
            return _isTriggered;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isTriggered && collisionLayerMask.Contains(other.gameObject.layer))
            {
                _isTriggered = true;
                OnEventTriggered();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!singleUse && collisionLayerMask.Contains(other.gameObject.layer))
            {
                _isTriggered = false;
            }
        }

        private void OnEventTriggered()
        {
            EventTriggered?.Invoke(this, EventArgs.Empty);
        }
    }
}