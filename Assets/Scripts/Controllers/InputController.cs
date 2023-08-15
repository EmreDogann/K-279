using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public abstract class InputController : ScriptableObject
    {
        public abstract void SetPlayerInput(PlayerInput playerInput);
        public abstract float RetrieveMoveInput();
        public abstract bool RetrieveInteractInput();
    }
}