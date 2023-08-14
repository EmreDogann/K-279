using System.Collections.Generic;
using UnityEngine;
using TNRD;

public class Button : MonoBehaviour, IInteractableObjects
{
    [SerializeField] private List<SerializableInterface<IReactableObjects>> reactables = null;

    private bool isButtonOn = false;
    public void InteractionContinues(bool isInteractionKeyDown)
    {
        if (isInteractionKeyDown)
        {
            if (isButtonOn)
            {
                reactables.ForEach(c =>  c.Value?.ReactionEventStart());
            } else
            {
                reactables.ForEach(c => c.Value?.ReactionEventEnd());
            }
            
            isButtonOn = !isButtonOn;
        } 
    }

    public void InteractionEnd()
    {
        return;
    }

    public void InteractionStart()
    {
        return;
    }

    public void RegisterInteractable()
    {
        throw new System.NotImplementedException();
    }
}