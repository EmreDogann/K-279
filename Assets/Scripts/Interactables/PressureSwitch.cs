using System.Collections.Generic;
using AYellowpaper;
using UnityEngine;

namespace Interactables
{
    public class PressureSwitch : MonoBehaviour, IInteractableObjects
    {
        [SerializeField] private List<InterfaceReference<IReactableObjects, MonoBehaviour>> reactables;
        [SerializeField] private bool isInteractable;

        public void InteractionContinues() {}

        public void InteractionStart() {}
        public void InteractionEnd() {}

        public void InteractionAreaEnter()
        {
            reactables.ForEach(c => c.Value?.ReactionEventStart());
        }

        public void InteractionAreaExit()
        {
            reactables.ForEach(c => c.Value?.ReactionEventEnd());
        }

        public bool IsInteractable()
        {
            return isInteractable;
        }
    }
}