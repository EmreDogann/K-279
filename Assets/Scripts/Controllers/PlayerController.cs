using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    [CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
    public class PlayerController : InputController
    {
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction _sprintAction;
        private InputAction _fireAction;
        private InputAction _interactAction;
        private InputAction _reloadAction;

        public override void SetPlayerInput(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _moveAction = _playerInput.actions["Move"];
            _sprintAction = _playerInput.actions["Sprint"];
            _fireAction = _playerInput.actions["Fire"];
            _interactAction = _playerInput.actions["Interact"];
            _reloadAction = _playerInput.actions["Reload"];
        }

        public override Vector2 GetMoveInput(GameObject gameObject)
        {
            return _moveAction.ReadValue<Vector2>();
        }

        public override bool IsSprintPressed()
        {
            return _sprintAction.IsPressed();
        }

        public override bool GetShootInput()
        {
            return _fireAction.IsPressed();
        }

        public override bool GetReloadInput()
        {
            return _reloadAction.IsPressed();
        }

        public override bool IsInteractPressed()
        {
            return _interactAction.WasPressedThisFrame();
        }

        public override bool GetInteractInput()
        {
            return _interactAction.IsPressed();
        }

        public override bool IsInteractReleased()
        {
            return _interactAction.WasReleasedThisFrame();
        }
    }
}