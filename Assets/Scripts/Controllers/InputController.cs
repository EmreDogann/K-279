using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract float RetrieveMoveInput(GameObject gameObject);
    public abstract bool RetrieveJumpInput();
    public abstract bool RetrieveInteractInput();
    public abstract bool RetrieveShootInput();
}
