using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
public class AIController : InputController
{
    public override bool RetrieveInteractInput()
    {
        return true;
    }

    public override bool RetrieveJumpInput()
    {
        return true;
    }

    public override float RetrieveMoveInput()
    {
        return 1f;
    }

    
}
