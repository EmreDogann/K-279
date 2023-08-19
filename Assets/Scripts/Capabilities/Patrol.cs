using Checks;
using GameEntities;
using MyBox;
using UnityEngine;

namespace Capabilities
{
    [RequireComponent(typeof(EnemyEntity))] [RequireComponent(typeof(Ground))] [RequireComponent(typeof(Rigidbody))]
    public class Patrol : MonoBehaviour
    {
        [Header("Patrol")]
        [SerializeField] private GameObject patrolLimitRightObject;
        [SerializeField] private GameObject patrolLimitLeftObject;
        [SerializeField] private LayerMask levelWallLayer;
        [SerializeField] [Range(0, 5f)] private float patrolRandomness;
        [SerializeField] [Range(1, 20f)] private float rangeOfSight = 2f;
        [SerializeField] [Range(0, 5f)] private float timeTillReturnToPatrol;
        [SerializeField] private LayerMask layerToCatch;

        [Header("Movement")]
        [SerializeField] [Range(0, 10f)] private float _maxPatrolVelocity = 3f;
        [SerializeField] [Range(0, 20f)] private float _maxPatrolAcceleration = 20f;
        [SerializeField] [Range(0, 100f)] private float _maxPursueVelocity = 3f;
        [SerializeField] [Range(0, 100f)] private float _maxPursueAcceleration = 20f;

        [Header("Attack")]
        [SerializeField] [Range(0, 10f)] private float _attackRange = 2f;

        [Separator("Animation")]
        [SerializeField] private Animator _animator;

        private Rigidbody _body;
        private Ground _ground;
        private Ray rayOfSight;
        private SpriteRenderer _sprite;

        private Vector3 _initPosition;
        private Vector3 patrolLimitLeft, patrolLimitRight;
        public Vector3 _targetPosition;
        public Vector3 _finalTargetPosition;
        private Vector2 _direction, _desiredVelocity, _velocity;

        private float _maxSpeedChange;
        private float _returnToPatrolTimer;
        private float _dmgPerHit;
        private float _hitCoolDown;
        private float _hitTimer;

        private bool _isPaused;
        private bool _patrolActive;
        private bool _facingRight = true;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        private void Awake()
        {
            _sprite = GetComponentInChildren<SpriteRenderer>();
            _ground = GetComponent<Ground>();
            _body = GetComponent<Rigidbody>();
            _dmgPerHit = GetComponent<EnemyEntity>().GetDmg();
            _hitCoolDown = GetComponent<EnemyEntity>().GetHitCoolDown();

            _patrolActive = true;
            _hitTimer = _hitCoolDown;
            rayOfSight.origin = gameObject.transform.position + new Vector3(0, 4, 0);
            rayOfSight.direction = gameObject.transform.right;

            if (patrolLimitLeftObject != null)
            {
                patrolLimitLeft = patrolLimitLeftObject.transform.position;
            }
            else
            {
                Ray direction = new Ray(rayOfSight.origin, rayOfSight.direction * -1);
                if (Physics.Raycast(direction, out RaycastHit hitInfo, Mathf.Infinity, levelWallLayer))
                {
                    patrolLimitLeft = hitInfo.point;
                }
            }

            if (patrolLimitRightObject != null)
            {
                patrolLimitRight = patrolLimitRightObject.transform.position;
            }
            else
            {
                if (Physics.Raycast(rayOfSight, out RaycastHit hitInfo, Mathf.Infinity, levelWallLayer))
                {
                    patrolLimitRight = hitInfo.point;
                }
            }

            //_targetPosition = transform.position + new Vector3(patrolLimitRight.position.x, 0, 0);
            if (Random.Range(0, 1) > 0.5f)
            {
                _facingRight = true;
                _targetPosition = patrolLimitRight;

                rayOfSight.direction = gameObject.transform.right;
            }
            else
            {
                _targetPosition = patrolLimitLeft;
                rayOfSight.direction = gameObject.transform.right * -1;
                FlipEnemy();
            }

            _initPosition = transform.position;
        }

        private void Update()
        {
            rayOfSight.origin = gameObject.transform.position + new Vector3(0, 4, 0);
            RaycastHit hitInfo;

            Debug.DrawRay(rayOfSight.origin, rayOfSight.direction * rangeOfSight, Color.red);

            bool isHit = Physics.Raycast(rayOfSight, out hitInfo, rangeOfSight, layerToCatch);

            if (isHit)
            {
                Pursue(hitInfo.transform.gameObject);
                _patrolActive = false;
                _returnToPatrolTimer = 0;
            }
            else if (!_patrolActive)
            {
                _returnToPatrolTimer += Time.deltaTime;

                if (_returnToPatrolTimer >= timeTillReturnToPatrol)
                {
                    // Return to Patrol and reset all patrol variables
                    _patrolActive = true;
                    if (_facingRight)
                    {
                        Debug.Log("Go left");
                        _targetPosition = patrolLimitLeft;
                        rayOfSight.direction = gameObject.transform.right * -1;
                    }
                    else
                    {
                        Debug.Log("Go Right");
                        _targetPosition = patrolLimitRight;
                        rayOfSight.direction = gameObject.transform.right;
                    }

                    FlipEnemy();
                }
            }

            _animator.SetBool(IsWalking, _patrolActive);
            if (_patrolActive)
            {
                Move(_targetPosition, _maxPatrolVelocity, _maxPatrolAcceleration);
                if ((gameObject.transform.position - _targetPosition).magnitude < 0.1f)
                {
                    if (_facingRight)
                    {
                        Debug.Log("Go left");
                        _targetPosition = patrolLimitLeft;
                        rayOfSight.direction = gameObject.transform.right * -1;
                    }
                    else
                    {
                        Debug.Log("Go Right");
                        _targetPosition = patrolLimitRight;
                        rayOfSight.direction = gameObject.transform.right;
                    }

                    FlipEnemy();
                }
            }
        }

        private void FlipEnemy()
        {
            _sprite.flipX = !_sprite.flipX;
            _facingRight = !_facingRight;
        }

        private void Move(Vector3 target, float maxVelocity, float maxAcceleration)
        {
            _direction.x = target.x - transform.position.x;
            _direction.x = _direction.x > 0 ? 1 : -1;
            _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(maxVelocity - _ground.Friction, 0f);

            _velocity = _body.velocity;

            _maxSpeedChange = maxAcceleration * Time.deltaTime;
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

            _body.velocity = _velocity;
        }


        private void Pursue(GameObject pursueTarget)
        {
            Vector3 targetPos = pursueTarget.transform.position;
            Move(targetPos, _maxPursueVelocity, _maxPursueAcceleration);

            if ((targetPos - transform.position).magnitude < _attackRange)
            {
                _hitTimer += Time.deltaTime;
                if (_hitTimer >= _hitCoolDown)
                {
                    // Hit
                    Debug.Log("Attacking " + pursueTarget.name);
                    pursueTarget.GetComponent<IEntity>().TakeHit(GetComponent<EnemyEntity>().GetDmg());
                    _hitTimer = 0f;
                }
            }
        }
    }
}