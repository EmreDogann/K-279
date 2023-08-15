using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    [CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
    public class AIController : InputController
    {
        [Header("Interaction")]
        [SerializeField] private LayerMask _layerMask = -1;
        [Header("Ray")]
        [SerializeField] private float _bottomDistance = 1f;
        [SerializeField] private float _topDistance = 1f;
        [SerializeField] private float _xOffset = 1f;

        private RaycastHit2D _groundInfoBottom;
        private RaycastHit2D _groundInfoTop;

        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction _fireAction;
        private InputAction _aimAction;
        private InputAction _interactAction;

        public override void SetPlayerInput(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _moveAction = _playerInput.actions["Move"];
            _fireAction = _playerInput.actions["Fire"];
            _aimAction = _playerInput.actions["Aim"];
            _interactAction = _playerInput.actions["Interact"];
        }
        public override bool RetrieveInteractInput()
        {
            return true;
        }

        public override float RetrieveMoveInput(GameObject gameObject)
        {
            _groundInfoBottom = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + (_xOffset + gameObject.transform.localScale.x),
            gameObject.transform.position.y), Vector2.down, _bottomDistance, _layerMask);
            Debug.DrawRay(new Vector2(gameObject.transform.position.x + (_xOffset + gameObject.transform.localScale.x), gameObject.transform.position.y),
                Vector2.down * _bottomDistance, Color.black);

            _groundInfoTop = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + (_xOffset + gameObject.transform.localScale.x),
                gameObject.transform.position.y), Vector2.right * gameObject.transform.localScale.x, _topDistance, _layerMask);
            Debug.DrawRay(new Vector2(gameObject.transform.position.x + (_xOffset + gameObject.transform.localScale.x), gameObject.transform.position.y),
                Vector2.right * _topDistance * gameObject.transform.localScale, Color.black);
            if (_groundInfoTop.collider == true || _groundInfoBottom.collider == false)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
            }

            return gameObject.transform.localScale.x;
        }

        public override bool RetrieveShootInput()
        {
            return false;
        }

        public override bool RetrieveJumpInput()
        {
            return false;
        }
    }
}

