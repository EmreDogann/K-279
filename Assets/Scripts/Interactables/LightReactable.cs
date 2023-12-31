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
            activeAtStart = !activeAtStart;
            gameObject.SetActive(activeAtStart);
        }
    }
}