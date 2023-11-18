using Events;
using Items;
using UnityEngine;

namespace Interaction.Interactables
{
    [RequireComponent(typeof(Item))]
    public class ItemInteractable : InteractableBase
    {
        [SerializeField] private Item item;
        [SerializeField] private ItemEventChannelSO itemInteractedEvent;

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
            itemInteractedEvent.Raise(item);
            // item.OpenDoor();
        }

        public override void OnInteract() {}

        public override void OnEndInteract() {}

        public override void OnEndHover() {}
    }
}