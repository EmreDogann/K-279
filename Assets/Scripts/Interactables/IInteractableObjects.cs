namespace Interactables
{
    public interface IInteractableObjects
    {
        public void RegisterInteractable();
        public void InteractionStart();

        public void InteractionContinues(bool isInteractionKeyDown);
        public void InteractionEnd();
    }
}