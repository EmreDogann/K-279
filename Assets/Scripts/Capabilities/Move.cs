using Checks;
using Controllers;
using UnityEngine;

namespace Capabilities
{
    [RequireComponent(typeof(Controller))]
    public class Move : MonoBehaviour
    {
        [SerializeField][Range(0f, 100f)] private float _maxSpeed = 4f;
        [SerializeField][Range(0f, 100f)] private float _maxAcceleration = 35f;

        private Controller _controller;
        private SpriteRenderer _sprite;
        private Vector2 _direction, _desiredVelocity, _velocity;
        private Rigidbody _body;
        private Ground _ground;

        private bool _facingRight;
        private float _maxSpeedChange;

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _ground = GetComponent<Ground>();
            _controller = GetComponent<Controller>();
        }

        private void Update()
        {
            _direction.x = _controller.input.RetrieveMoveInput(gameObject);
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
            _sprite.flipX = !_sprite.flipX;
            _facingRight = !_facingRight;
        }
    }
}
