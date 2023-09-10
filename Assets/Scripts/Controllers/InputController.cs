using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public abstract class InputController : ScriptableObject
    {
        public abstract void SetPlayerInput(PlayerInput playerInput);
        public abstract float RetrieveMoveInput(GameObject game);
        public abstract bool RetrieveShootInput();
        public abstract bool RetrieveReloadInput();
        public abstract bool RetrieveInteractInput();
    }
}