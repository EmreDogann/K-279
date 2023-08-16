namespace Interactables
{
    public interface IInteractableObjects
    {
        public void InteractionStart();

        public void InteractionContinues(bool isInteractionKeyDown);
        public void InteractionEnd();
    }
}