using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    [CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
    public class AIController : InputController
    {
        public override void SetPlayerInput(PlayerInput playerInput) {}

        public override bool RetrieveInteractPress()
        {
            return true;
        }

        public override bool RetrieveInteractInput()
        {
            return true;
        }

        public override bool RetrieveInteractRelease()
        {
            return true;
        }

        public override float RetrieveMoveInput(GameObject gameObject)
        {
            return 0f;
        }

        public override bool RetrieveShootInput()
        {
            return false;
        }

        public override bool RetrieveReloadInput()
        {
            throw new NotImplementedException();
        }
    }
}