using Capabilities.Movement;
using Checks;
using Controllers;
using Events;
using MyBox;
using Rooms;
using UnityEngine;

namespace Capabilities
{
    [RequireComponent(typeof(Controller))]
    public class Move : MonoBehaviour, IMover
    {
        [Separator("Movement")]
        [SerializeField] [Range(0f, 100f)] private float maxSpeed = 4f;
        [SerializeField] [Range(0f, 100f)] private float maxAcceleration = 35f;

        [Separator("Other")]
        [SerializeField] private Animator animator;
        [SerializeField] private BoolEventListener pauseEvent;

        private Controller _controller;
        private SpriteRenderer _sprite;
        private Vector2 _direction, _desiredVelocity, _velocity;
        private Rigidbody _body;
        private Ground _ground;

        private bool _isPaused;
        private bool _facingRight;
        private bool _movementActive;
        private float _maxSpeedChange;
        private readonly int _isWalking = Animator.StringToHash("IsWalking");
        private readonly int _isShooting = Animator.StringToHash("IsShooting");

        public event SwitchingDirection OnSwitchingDirection;
        event SwitchingDirection IMover.OnSwitchingDirection
        {
            add => OnSwitchingDirection += value;
            remove => OnSwitchingDirection -= value;
        }

        private void Awake()
        {
            _facingRight = true;
            _movementActive = true;
            ;
            _body = GetComponent<Rigidbody>();
            _ground = GetComponent<Ground>();
            _controller = GetComponent<Controller>();
            _sprite = GetComponentInChildren<SpriteRenderer>();
        }

        private void OnEnable()
        {
            pauseEvent.Response.AddListener(OnPauseEvent);
        }

        private void OnDisable()
        {
            pauseEvent.Response.RemoveListener(OnPauseEvent);
        }

        private void OnPauseEvent(bool isPaused)
        {
            _isPaused = isPaused;
        }

        private void Update()
        {
            if (_isPaused)
            {
                return;
            }

            if (!_movementActive)
            {
                return;
            }

            _direction.x = _controller.input.GetMoveInput(gameObject).x;
            animator.SetBool(_isWalking, _direction.x != 0);
            animator.SetBool(_isShooting, false);
            //if (_direction.x > 0 && !_facingRight)
            //{
            //    FlipPlayer();
            //}
            //else if (_direction.x < 0f && _facingRight)
            //{
            //    FlipPlayer();
            //}

            _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(maxSpeed - _ground.Friction, 0f);
        }

        private void FixedUpdate()
        {
            _velocity = _body.velocity;

            _maxSpeedChange = maxAcceleration * Time.deltaTime;
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

            _body.velocity = _velocity;
        }

        public void SwitchDirection()
        {
            //_sprite.flipX = !_sprite.flipX;
            _sprite.gameObject.transform.Rotate(0, -180, 0, Space.Self);
            _facingRight = !_facingRight;

            OnSwitchingDirection?.Invoke(_facingRight);
        }

        public void SetMovementParams(RoomData roomData)
        {
            if (roomData.StartingPosition != null)
            {
                transform.position = roomData.StartingPosition.position;
            }
        }

        public void StartMovement()
        {
            _movementActive = true;
        }

        public void StopMovement()
        {
            _movementActive = false;
            _body.velocity = Vector3.zero;
            _desiredVelocity = Vector3.zero;
        }
    }
}