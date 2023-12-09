using Interactables;
using UnityEngine;

namespace Interaction.Interactables
{
    [RequireComponent(typeof(Door))]
    public class DoorInteractable : InteractableBase
    {
        [SerializeField] private Door door;

        private void Awake()
        {
            door = GetComponent<Door>();
        }

        private void Reset()
        {
            door = GetComponent<Door>();
        }

        public override void OnStartHover(IInteractor interactor) {}

        public override void OnStartInteract(IInteractor interactor)
        {
            base.OnStartInteract(interactor);
            switch (interactor.ResolveInteraction(door))
            {
                case ItemUserInteractionType.Default:
                    door.OpenDoor();
                    break;
            }
        }

        public override void OnInteract(IInteractor interactor) {}

        public override void OnEndInteract(IInteractor interactor) {}

        public override void OnEndHover(IInteractor interactor) {}
    }
}