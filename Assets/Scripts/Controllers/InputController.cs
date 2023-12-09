using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public abstract class InputController : ScriptableObject
    {
        public abstract void SetPlayerInput(PlayerInput playerInput);
        public abstract Vector2 GetMoveInput(GameObject gameObj);
        public abstract bool IsSprintPressed();
        public abstract bool GetShootInput();
        public abstract bool GetReloadInput();
        public abstract bool IsInteractPressed();
        public abstract bool GetInteractInput();
        public abstract bool IsInteractReleased();
    }
}