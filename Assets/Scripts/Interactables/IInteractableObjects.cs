using UnityEngine;


public interface IInteractableObjects
{
    public abstract void RegisterInteractable();
    public abstract void InteractionStart();

    public abstract void InteractionContinues(bool isInteractionKeyDown);
    public abstract void InteractionEnd();
}