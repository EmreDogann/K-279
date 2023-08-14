using UnityEngine;
using System.Collections.Generic;
using TNRD;

public class PressureSwitch : MonoBehaviour, IInteractableObjects
{

    [SerializeField] private List<SerializableInterface<IReactableObjects>> reactables = null;
    public void InteractionContinues(bool isInteractionKeyDown)
    {
        //if (isInteractionKeyDown) Debug.Log("Button Pressed");
        return;
    }

    public void InteractionEnd()
    {
        reactables.ForEach(c => c.Value?.ReactionEventStart());
    }
    
    public void InteractionStart()
    {

        reactables.ForEach(c => c.Value?.ReactionEventEnd());
    }

    public void RegisterInteractable()
    {
        throw new System.NotImplementedException();
    }
}