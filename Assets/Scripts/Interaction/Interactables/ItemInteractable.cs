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

        public override void OnStartHover(IInteractor interactor) {}

        public override void OnStartInteract(IInteractor interactor)
        {
            base.OnStartInteract(interactor);
            itemInteractedEvent.Raise(item);
        }

        public override void OnInteract(IInteractor interactor) {}

        public override void OnEndInteract(IInteractor interactor) {}

        public override void OnEndHover(IInteractor interactor) {}
    }
}