using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "New Item Event", menuName = "Game Event/Item Event", order = 3)]
    public class ItemEventChannelSO : ScriptableObject
    {
        private readonly List<ItemEventListener> listeners = new List<ItemEventListener>();

        public void Raise(IItem value)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(value);
            }
        }

        public void RegisterListener(ItemEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(ItemEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}