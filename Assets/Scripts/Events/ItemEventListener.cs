using System;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    ///     To use a generic UnityEvent type you must override the generic type.
    /// </summary>
    [Serializable]
    public class ItemEvent : UnityEvent<IItem> {}

    public class ItemEventListener : MonoBehaviour
    {
        public ItemEventChannelSO Event;
        public ItemEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(IItem value)
        {
            Response.Invoke(value);
        }
    }
}