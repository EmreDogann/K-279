using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public override float RetrieveMoveInput()
    {
        Debug.Log("Moving");
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool RetrieveInteractInput()
    {
        return Input.GetButtonDown("Interact");
    }
}