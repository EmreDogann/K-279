using System;
using UnityEngine;

namespace Interactables
{
    public class LightReactable : MonoBehaviour, IReactableObjects
    {
        public bool activeAtStart;

        private void Awake()
        {
            gameObject.SetActive(activeAtStart);
        }

        public void ReactionEventEnd()
        {
            activeAtStart = !activeAtStart;
            gameObject.SetActive(activeAtStart);
        }

        public void ReactionEventStart()
        {
            Debug.Log("End");
            activeAtStart = !activeAtStart;
            gameObject.SetActive(activeAtStart);
        }

        void IReactableObjects.RegisterReactable()
        {
            throw new NotImplementedException();
        }
    }
}