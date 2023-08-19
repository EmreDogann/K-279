using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace Interactables
{
    public class PressureSwitch : MonoBehaviour, IInteractableObjects
    {
        [SerializeField] private List<SerializableInterface<IReactableObjects>> reactables;
        [SerializeField] private bool isInteractable;

        public bool InteractionContinues(bool isInteractionKeyDown)
        {
            //if (isInteractionKeyDown) Debug.Log("Button Pressed");
            return true;
        }

        public void InteractionEnd()
        {
            reactables.ForEach(c => c.Value?.ReactionEventStart());
        }

        public bool IsInteractable()
        {
            return isInteractable;
        }

        public void InteractionStart()
        {
            reactables.ForEach(c => c.Value?.ReactionEventEnd());
        }
    }
}