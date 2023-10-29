using Capabilities.Movement;
using Checks;
using Controllers;
using Events;
using MyBox;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Vertx.Debugging;

namespace Capabilities
{
    [RequireComponent(typeof(Controller))]
    public class MoveFollowPath : MonoBehaviour, IMover
    {
        [Separator("Movement")]
        [SerializeField] [Range(0f, 100f)] private float maxSpeed = 4f;
        [SerializeField] [Range(0f, 100f)] private float maxAcceleration = 35f;

        [Separator("Other")]
        [SerializeField] private Animator animator;
        [SerializeField] private SplineContainer path;
        [SerializeField] private BoolEventListener pauseEvent;

        [Separator("Debug")]
        [SerializeField] private bool enableDebug;

        private Controller _controller;
        private SpriteRenderer _sprite;
        private Vector3 _direction, _desiredVelocity, _velocity;
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
            if (_isPaused || !_movementActive)
            {
                return;
            }

            _direction.x = _controller.input.RetrieveMoveInput(gameObject).x;
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

            float distance =
                SplineUtility.GetNearestPoint(path.Spline, path.transform.InverseTransformPoint(transform.position),
                    out float3 nearestPoint, out float t);

            if (distance > 0.01f)
            {
                transform.position = path.transform.TransformPoint(nearestPoint);
            }

            Vector3 forward = Vector3.Normalize(path.EvaluateTangent(t));
            forward *= _direction.x;


            _desiredVelocity = forward * Mathf.Max(maxSpeed - _ground.Friction, 0f);

            if (enableDebug)
            {
                D.raw(new Shape.Text(transform.position - new Vector3(0, 0, 1.0f), $"Path %: {t}")); // Path percentage
                D.raw(new Shape.Arrow(transform.position, forward), Color.blue); // Forward direction
                D.raw(new Shape.Arrow(transform.position, _desiredVelocity), Color.red); // Target velocity
                D.raw(new Shape.Arrow(transform.position, _body.velocity), Color.green); // Current velocity
            }
        }

        private void FixedUpdate()
        {
            _velocity = _body.velocity;

            _maxSpeedChange = maxAcceleration * Time.deltaTime;
            _velocity = Vector3.MoveTowards(_velocity, _desiredVelocity, _maxSpeedChange);

            _body.velocity = new Vector3(_velocity.x, _body.velocity.y, _velocity.z);
        }

        public void SwitchDirection()
        {
            //_sprite.flipX = !_sprite.flipX;
            _sprite.gameObject.transform.Rotate(0, -180, 0, Space.Self);
            _facingRight = !_facingRight;

            OnSwitchingDirection?.Invoke(_facingRight);
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