using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    [CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
    public class AIController : InputController
    {
        public override void SetPlayerInput(PlayerInput playerInput) {}

        public override bool IsInteractPressed()
        {
            return true;
        }

        public override bool GetInteractInput()
        {
            return true;
        }

        public override bool IsInteractReleased()
        {
            return true;
        }

        public override Vector2 GetMoveInput(GameObject gameObject)
        {
            return Vector2.zero;
        }

        public override bool IsSprintPressed()
        {
            return false;
        }

        public override bool GetShootInput()
        {
            return false;
        }

        public override bool GetReloadInput()
        {
            throw new NotImplementedException();
        }
    }
}