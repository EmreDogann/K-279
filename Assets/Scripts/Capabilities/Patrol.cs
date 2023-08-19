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
        [SerializeField] private bool _pursueActive = true;
        [SerializeField] private bool _patrolActive = true;
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

        private EnemyEntity _enemyEntity;
        private Rigidbody _body;
        private Ground _ground;
        private Ray rayOfSight;
        private SpriteRenderer _sprite;

        private Vector3 _initPosition;
        private Vector3 patrolLimitLeft, patrolLimitRight;
        public Vector3 _targetPosition;
        private Vector2 _direction, _desiredVelocity, _velocity;

        private float _maxSpeedChange;
        private float _returnToPatrolTimer;
        private float _dmgPerHit;
        private float _hitCoolDown;
        private float _hitTimer;

        private bool _facingRight = true;
        private bool _isPaused;
      
        
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        private static readonly int AttackState = Animator.StringToHash("EnemyAttack");

        private void Awake()
        {
            _sprite = GetComponentInChildren<SpriteRenderer>();
            _ground = GetComponent<Ground>();
            _body = GetComponent<Rigidbody>();
            _enemyEntity = GetComponent<EnemyEntity>();
            _dmgPerHit = _enemyEntity.GetDmg();
            _hitCoolDown = _enemyEntity.GetHitCoolDown();
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
                if (Physics.Raycast(rayOfSight.origin, transform.right, out RaycastHit hitInfo, Mathf.Infinity, levelWallLayer))
                {
                    patrolLimitRight = hitInfo.point;
                }
            }

            //_targetPosition = transform.position + new Vector3(patrolLimitRight.position.x, 0, 0);
            if (Random.Range(0, 1) > 0.5f)
            {
                Debug.Log("Pointing Right");
                _facingRight = true;
                _targetPosition = patrolLimitRight;

                rayOfSight.direction = gameObject.transform.right;
            }
            else
            {
                Debug.Log("Pointing Left");
                _targetPosition = patrolLimitLeft;
                rayOfSight.direction = gameObject.transform.right * -1;
                FlipEnemy();
            }

            _initPosition = transform.position;
        }

        private void Update()
        {
            if (!_enemyEntity.IsAlive() || (!_patrolActive & !_pursueActive)) return;
            rayOfSight.origin = gameObject.transform.position + new Vector3(0, 4, 0);
            RaycastHit hitInfo;

            Debug.DrawRay(rayOfSight.origin, rayOfSight.direction * rangeOfSight, Color.red);

            

            if (_pursueActive)
            {
                bool isHit = Physics.Raycast(rayOfSight, out hitInfo, rangeOfSight, layerToCatch);

                if (isHit)
                {
                    Debug.Log("Pursuing");
                    Pursue(hitInfo.transform.gameObject);
                    _patrolActive = false;
                    _returnToPatrolTimer = 0;
                }
            }
            else if (!_patrolActive )
            {
                _returnToPatrolTimer += Time.deltaTime;

                if (_returnToPatrolTimer >= timeTillReturnToPatrol)
                {
                    // Return to Patrol and reset all patrol variables
                    _patrolActive = true;
                    if (_facingRight)
                    {
                        Debug.Log("Go left : pursue");
                        _targetPosition = patrolLimitLeft;
                        rayOfSight.direction = gameObject.transform.right * -1;
                    }
                    else
                    {
                        Debug.Log("Go Right : pursue");
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
            Debug.Log(_facingRight);
            _sprite.flipX = !_sprite.flipX;
            _facingRight = !_facingRight;
            Debug.Log(_facingRight);

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
                    _animator.SetTrigger(AttackState);
                    pursueTarget.GetComponent<IEntity>().TakeHit(GetComponent<EnemyEntity>().GetDmg());
                    _hitTimer = 0f;
                }
            }
        }
    }
}