using UnityEngine;


public interface IReactableObjects
{
    public abstract void RegisterReactable();
    public abstract void ReactionEventStart();
    public abstract void ReactionEventEnd();
}