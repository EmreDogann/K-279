using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public class Controller : MonoBehaviour
    {
        
        public InputController input;
        private void Awake()
        {
            if (input.GetType() == typeof(PlayerController) && GetComponent<PlayerInput>() != null)
            {
                input.SetPlayerInput(GetComponent<PlayerInput>());
            }
            
        }
    }
}