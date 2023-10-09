namespace Interactables
{
    public interface IInteractableObjects
    {
        public void InteractionStart();
        public void InteractionContinues();
        public void InteractionEnd();
        public void InteractionAreaEnter();
        public void InteractionAreaExit();
        public bool IsInteractable();
    }
}