using Checks;
using Controllers;
using UnityEngine;

namespace Capabilities
{
    [RequireComponent(typeof(Controller))]
    public class Move : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [SerializeField] [Range(0f, 100f)] private float _maxSpeed = 4f;
        [SerializeField] [Range(0f, 100f)] private float _maxAcceleration = 35f;

        private Controller _controller;
        private SpriteRenderer _sprite;
        private Vector2 _direction, _desiredVelocity, _velocity;
        private Rigidbody _body;
        private Ground _ground;

        private bool _facingRight;
        private float _maxSpeedChange;
        private readonly int _isWalking = Animator.StringToHash("IsWalking");

        private void Awake()
        {
            _facingRight = true;
            _body = GetComponent<Rigidbody>();
            _ground = GetComponent<Ground>();
            _controller = GetComponent<Controller>();
            _sprite = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            _direction.x = _controller.input.RetrieveMoveInput(gameObject);
            _animator.SetBool(_isWalking, _direction.x != 0);

            if (_direction.x > 0 && !_facingRight)
            {
                FlipPlayer();
            }
            else if (_direction.x < 0f && _facingRight)
            {
                FlipPlayer();
            }

            _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _ground.Friction, 0f);
        }

        private void FixedUpdate()
        {
            _velocity = _body.velocity;

            _maxSpeedChange = _maxAcceleration * Time.deltaTime;
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

            _body.velocity = _velocity;
        }

        private void FlipPlayer()
        {
            //_sprite.flipX = !_sprite.flipX;
            _sprite.gameObject.transform.Rotate(0, -180, 0, Space.Self);
            _facingRight = !_facingRight;
        }
    }
}