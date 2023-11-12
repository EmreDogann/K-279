using Items;
using UnityEngine;

namespace Interaction.Interactables
{
    [RequireComponent(typeof(Item))]
    public class ItemInteractable : InteractableBase
    {
        [SerializeField] private Item item;

        private void Awake()
        {
            item = GetComponent<Item>();
        }

        private void Reset()
        {
            item = GetComponent<Item>();
        }

        public override void OnStartHover() {}

        public override void OnStartInteract()
        {
            base.OnStartInteract();
            // item.OpenDoor();
        }

        public override void OnInteract() {}

        public override void OnEndInteract() {}

        public override void OnEndHover() {}
    }
}