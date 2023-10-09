using UnityEngine;

namespace Interactables
{
    public class ItemInteractable : MonoBehaviour, IInteractableObjects
    {
        [SerializeField] private bool isInteractable = true;

        public void InteractionStart() {}
        public void InteractionContinues() {}
        public void InteractionEnd() {}

        public void InteractionAreaEnter() {}
        public void InteractionAreaExit() {}

        public bool IsInteractable()
        {
            return isInteractable;
        }
    }
}