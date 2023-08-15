using System;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace Interactables
{
    public class Button : MonoBehaviour, IInteractableObjects
    {
        [SerializeField] private List<SerializableInterface<IReactableObjects>> reactables;

        private bool isButtonOn;

        public void InteractionContinues(bool isInteractionKeyDown)
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
            }
        }

        public void InteractionEnd() {}

        public void InteractionStart() {}

        public void RegisterInteractable()
        {
            throw new NotImplementedException();
        }
    }
}