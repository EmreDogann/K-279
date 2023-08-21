using UnityEngine;

namespace Interactables
{
    public class ItemInteractable : MonoBehaviour, IInteractableObjects
    {
        [SerializeField] private bool isInteractable = true;

        public bool InteractionContinues(bool isInteractionKeyDown)
        {
            return true;
        }

        public void InteractionEnd() {}

        public bool IsInteractable()
        {
            return isInteractable;
        }

        public void InteractionStart() {}
    }
}