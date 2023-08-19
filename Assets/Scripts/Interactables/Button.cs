using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace Interactables
{
    public class Button : MonoBehaviour, IInteractableObjects
    {
        [SerializeField] private List<SerializableInterface<IReactableObjects>> reactables;
        [SerializeField] private bool isInteractable;

        private bool isButtonOn;

        public bool InteractionContinues(bool isInteractionKeyDown)
        {
            if (isInteractionKeyDown)
            {
                if (isButtonOn)
                {
                    reactables.ForEach(c => c.Value?.ReactionEventStart());
                }
                else
                {
                    reactables.ForEach(c => c.Value?.ReactionEventEnd());
                }

                isButtonOn = !isButtonOn;
                return true;
            }

            return false;
        }

        public void InteractionEnd() {}

        public bool IsInteractable()
        {
            return isInteractable;
        }

        public void InteractionStart() {}
    }
}