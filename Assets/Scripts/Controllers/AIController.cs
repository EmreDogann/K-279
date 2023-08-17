using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Controllers
{
    [CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
    public class AIController : InputController
    {
        // [Header("Interaction")]
        // [SerializeField] private LayerMask _layerMask = -1;
        // [Header("Ray")]
        // [SerializeField] private float _bottomDistance = 1f;
        // [SerializeField] private float _topDistance = 1f;
        // [SerializeField] private float _xOffset = 1f;

        private RaycastHit _groundInfoBottom;
        private RaycastHit _groundInfoTop;

        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction _fireAction;
        private InputAction _aimAction;
        private InputAction _interactAction;

        public override void SetPlayerInput(PlayerInput playerInput) {}

        public override bool RetrieveInteractInput()
        {
            return true;
        }

        public override float RetrieveMoveInput(GameObject gameObject)
        {
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();


            if (agent.isActiveAndEnabled)
            {
                //agent.SetDestination();
                return agent.velocity.x;
            }

            return 0f;
        }

        public override bool RetrieveShootInput()
        {
            return false;
        }
    }
}