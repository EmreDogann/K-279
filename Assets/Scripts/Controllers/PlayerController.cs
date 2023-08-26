using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    [CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
    public class PlayerController : InputController
    {
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction _fireAction;
        private InputAction _interactAction;
        private InputAction _reloadAction;

        public override void SetPlayerInput(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _moveAction = _playerInput.actions["Move"];
            _fireAction = _playerInput.actions["Fire"];
            _interactAction = _playerInput.actions["Interact"];
            _reloadAction = _playerInput.actions["Reload"];
        }

        public override float RetrieveMoveInput(GameObject gameObject)
        {
            return _moveAction.ReadValue<Vector2>().x;
        }

        public override bool RetrieveInteractInput()
        {
            return _interactAction.WasPressedThisFrame();
        }

        public override bool RetrieveShootInput()
        {
            return _fireAction.IsPressed();
        }

        public override bool RetrieveReloadInput()
        {
            return _reloadAction.IsPressed();
        }
    }
}