using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public override float RetrieveMoveInput(GameObject gameObject)
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool RetrieveInteractInput()
    {
        return Input.GetButtonDown("Interact");
    }

    public override bool RetrieveShootInput()
    {
        return Input.GetButtonDown("Shoot");
    }
}