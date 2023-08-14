using UnityEngine;

[CreateAssetMenu(fileName = "InteractableObject")]
public abstract class InteractableObjects: ScriptableObject
{
    public abstract void InteractionStart();

    public abstract void InteractionContinues(bool isInteractionKeyDown);
    public abstract void InteractionEnd();
}