using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    [CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
    public class AIController : InputController
    {
        private PlayerInput _playerInput;

        public override void SetPlayerInput(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public override bool RetrieveInteractInput()
        {
            return true;
        }

        public override float RetrieveMoveInput()
        {
            return 1f;
        }
    }
}