using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace Interactables
{
    public class Button : MonoBehaviour, IInteractableObjects
    {
        [SerializeField] private List<SerializableInterface<IReactableObjects>> reactables;
        [SerializeField] private bool isInteractable;


        public void InteractionContinues() {}

        public void InteractionStart()
        {
            reactables.ForEach(c => c.Value?.ReactionEventStart());
        }

        public void InteractionEnd()
        {
            reactables.ForEach(c => c.Value?.ReactionEventEnd());
        }

        public void InteractionAreaEnter() {}
        public void InteractionAreaExit() {}

        public bool IsInteractable()
        {
            return isInteractable;
        }
    }
}