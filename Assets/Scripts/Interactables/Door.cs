using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace Interactables
{
    public class Door : MonoBehaviour, IInteractableObjects
    {
        [SerializeField] private List<SerializableInterface<IReactableObjects>> reactables;

        private bool _isButtonOn;

        public void InteractionContinues(bool isInteractionKeyDown)
        {
            if (isInteractionKeyDown)
            {
                if (_isButtonOn)
                {
                    reactables.ForEach(c => c.Value?.ReactionEventStart());
                }
                else
                {
                    reactables.ForEach(c => c.Value?.ReactionEventEnd());
                }

                _isButtonOn = !_isButtonOn;
            }
        }

        public void InteractionEnd() {}

        public void InteractionStart() {}
    }
}