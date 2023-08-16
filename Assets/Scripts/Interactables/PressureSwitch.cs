using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace Interactables
{
    public class PressureSwitch : MonoBehaviour, IInteractableObjects
    {
        [SerializeField] private List<SerializableInterface<IReactableObjects>> reactables;

        public void InteractionContinues(bool isInteractionKeyDown)
        {
            //if (isInteractionKeyDown) Debug.Log("Button Pressed");
        }

        public void InteractionEnd()
        {
            reactables.ForEach(c => c.Value?.ReactionEventStart());
        }

        public void InteractionStart()
        {
            reactables.ForEach(c => c.Value?.ReactionEventEnd());
        }
    }
}