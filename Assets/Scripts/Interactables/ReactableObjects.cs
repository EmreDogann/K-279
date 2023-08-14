using UnityEngine;

[CreateAssetMenu(fileName = "ReactableObject")]
public abstract class ReactableObjects : ScriptableObject
{

    public int interactableID;

    public abstract void ReactionEvent();
}