using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class VoidEventListener : MonoBehaviour
    {
        public VoidEventChannelSO Event;
        public UnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}