using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    [RequireComponent(typeof(PlayerInput))]
    public class Controller : MonoBehaviour
    {
        private PlayerInput _playerInput;
        public InputController input;

        private void Awake()
        {
            input.SetPlayerInput(GetComponent<PlayerInput>());
        }
    }
}