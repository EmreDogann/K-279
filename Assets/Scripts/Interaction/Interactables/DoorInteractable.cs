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

        public override void OnStartHover() {}

        public override void OnStartInteract()
        {
            base.OnStartInteract();
            door.OpenDoor();
        }

        public override void OnInteract() {}

        public override void OnEndInteract() {}

        public override void OnEndHover() {}
    }
}