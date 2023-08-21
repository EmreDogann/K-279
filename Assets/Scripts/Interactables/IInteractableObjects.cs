namespace Interactables
{
    public interface IInteractableObjects
    {
        public void InteractionStart();
        public bool InteractionContinues(bool isInteractionKeyDown);
        public void InteractionEnd();
        public bool IsInteractable();
    }
}