using Interactables;
using UnityEngine;

namespace Interaction.Interactables
{
    [RequireComponent(typeof(FuseSlot))]
    public class FuseSlotInteractable : InteractableBase
    {
        [SerializeField] private FuseSlot fuseSlot;

        private void Awake()
        {
            fuseSlot = GetComponent<FuseSlot>();
        }

        private void Reset()
        {
            fuseSlot = GetComponent<FuseSlot>();
        }

        public override void OnStartHover(IInteractor interactor) {}

        public override void OnStartInteract(IInteractor interactor)
        {
            base.OnStartInteract(interactor);
            switch (interactor.ResolveInteraction(fuseSlot))
            {
                case ItemUserInteractionType.Default:
                    fuseSlot.TryItem(null);
                    break;
            }
        }

        public override void OnInteract(IInteractor interactor) {}

        public override void OnEndInteract(IInteractor interactor) {}

        public override void OnEndHover(IInteractor interactor) {}
    }
}