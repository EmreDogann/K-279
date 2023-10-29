using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public abstract class InputController : ScriptableObject
    {
        public abstract void SetPlayerInput(PlayerInput playerInput);
        public abstract Vector2 RetrieveMoveInput(GameObject game);
        public abstract bool RetrieveShootInput();
        public abstract bool RetrieveReloadInput();
        public abstract bool RetrieveInteractPress();
        public abstract bool RetrieveInteractInput();
        public abstract bool RetrieveInteractRelease();
    }
}