using Capabilities;
using Checks;
using MyBox;
using Rooms;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Splines;
using UnityHFSM;

namespace EnemyAI
{
    public enum EnemyStates
    {
        WANDER,
        INVESTIGATE,
        PATROL,
        CHASE,
        ATTACK,
    }
    public enum EnemyRoom
    {
        Main,
        Side,
        Other
    }
    public class StalkerAI : MonoBehaviour
    {

        [Separator("Investigate Parameters")]
        [SerializeField] private float timeToEscalation = 2f;
        [SerializeField] private float investigateDuration = 5f;

        [Separator("Patrol Parameters")]
        [SerializeField] private LayerMask layerToCatch;
        [SerializeField] private float patrolRange = 10f;
        [SerializeField] private float rangeOfSight = 5f;
        [SerializeField] private float patrolSpeed = 2;
        [SerializeField] private float patrolDuration = 10f;

        [Separator("Chase Parameters")]
        [SerializeField] private float chaseSpeed = 4;
        [SerializeField] private float abandonChaseDuration = 4f;

        [Separator("Attack Parameters")]
        [SerializeField] private float attackSpeed = 2;
        [SerializeField] private float attackRange = 3;

        [Separator("Testing")]
        [SerializeField] private EnemyStates currentEnemyState;
        [SerializeField] private bool useSplinePaths;
        [SerializeField] private bool useLineOfSight;

        private StateMachine<EnemyStates> _fsm;
        private Transform playerTransform;
        private SplineContainer _path;
        private SpriteRenderer _stalkerSprite;
        private Ground _ground;
        private Rigidbody _body;

        private RoomType _currentRoom;

        private Vector3 _desiredVelocity, _velocity;
        private Vector2 _direction, patrolStartPosition;

        private int _currentPathIndex;
        private float _patrolOffset = 0f;
        private bool _facingRight;
        private void OnEnable()
        {
            AudioAlert.OnTriggerAudioAlert += AudioAlert_OnTriggerAudioAlert;
        }
        private void OnDisable()
        {
            AudioAlert.OnTriggerAudioAlert -= AudioAlert_OnTriggerAudioAlert;
        }
        private void AudioAlert_OnTriggerAudioAlert(AudioAlertType arg1, Transform arg2)
        {
            if (_fsm.ActiveState.name == EnemyStates.WANDER)
            {
                Debug.Log("AudioAlertOne");
                _fsm.Trigger("AudioAlertOne");
            } else if (_fsm.ActiveState.name == EnemyStates.INVESTIGATE)
            {
                
                Debug.Log("AudioAlertTwo");
                _fsm.Trigger("AudioAlertTwo");
            } else if (_fsm.ActiveState.name == EnemyStates.PATROL)
            {
                Debug.Log("AudioAlertThree");
                _fsm.Trigger("AudioAlertThree");
            }
            playerTransform = arg2;
        }

        private void Awake()
        {
            _stalkerSprite = GetComponentInChildren<SpriteRenderer>();
            _ground = GetComponent<Ground>();
            _body = GetComponent<Rigidbody>();
            _direction.x = 0;
            _facingRight = true;
        }

        private void Start()
        {

            _fsm = new StateMachine<EnemyStates>();

            // Defining FSM states

            // WANDER
            _fsm.AddState(EnemyStates.WANDER, new State<EnemyStates>(
                onEnter: state => {
                    // Make stalker sprite invisible in wandering state
                    _stalkerSprite.enabled = false;

                    // Place stalker in random room
                    Array enumValArray = Enum.GetValues(typeof(RoomType));
                    _currentRoom = (RoomType) enumValArray.GetValue(UnityEngine.Random.Range(0, enumValArray.Length));

                },
                onLogic: state => {
                    // Does nothing right now
                },
                canExit: state => state.timer.Elapsed > timeToEscalation, needsExitTime: true
                ));

            // INVESTIGATE
            _fsm.AddState(EnemyStates.INVESTIGATE, new State<EnemyStates>(
                onLogic: state => {
                    Debug.Log("Investigate: " + state.timer.Elapsed);

                }, 
                canExit: state => state.timer.Elapsed > timeToEscalation, needsExitTime: true));

            // PATROL
            _fsm.AddState(EnemyStates.PATROL, new State<EnemyStates>(
                onEnter: state => {
                    // Make enemy visible when it starts to chase
                    _stalkerSprite.enabled = true;

                    // Put Stalker on the same path as the player
                    _path = playerTransform.GetComponent<MoveFollowPath>().GetPlayerPath();
                    _currentPathIndex = playerTransform.GetComponent<MoveFollowPath>().GetCurrentPathIndex();
                },
                onLogic: state =>
                {

                    Patrol();
                },
                onExit: state => {
                    _direction.x = 0;
                    _body.velocity = Vector3.zero;
                    _desiredVelocity = Vector3.zero;
                }
                ));

            // CHASE
            _fsm.AddState(EnemyStates.CHASE, new State<EnemyStates>(
                onLogic: state => {

                    ChasePlayer();
                    },
                onExit: state => {
                    _direction.x = 0;
                    _body.velocity = Vector3.zero;
                    _desiredVelocity = Vector3.zero;
                }
                ));

            // ATTACK
            _fsm.AddState(EnemyStates.ATTACK, new State<EnemyStates>(
                onLogic: state => {
                    // Does nothing right now
                }));

            // Setting start state
            _fsm.SetStartState(EnemyStates.WANDER);


            // Set FSM transitions with conditions

            // WANDER <==> INVESTIGATE
            _fsm.AddTriggerTransition("AudioAlertOne", new Transition<EnemyStates>(EnemyStates.WANDER, EnemyStates.INVESTIGATE));
            _fsm.AddTransition(new TransitionAfter<EnemyStates>(EnemyStates.INVESTIGATE, EnemyStates.WANDER, investigateDuration));
            // INVESTIGATE <==> PATROL
            _fsm.AddTriggerTransition("AudioAlertTwo", new Transition<EnemyStates>(EnemyStates.INVESTIGATE, EnemyStates.PATROL));
            _fsm.AddTransition(new TransitionAfter<EnemyStates>(EnemyStates.PATROL, EnemyStates.INVESTIGATE, patrolDuration));

            // PATROL <==> CHASE
            _fsm.AddTriggerTransition("AudioAlertThree", new Transition<EnemyStates>(EnemyStates.PATROL, EnemyStates.CHASE));
            _fsm.AddTransition(new Transition<EnemyStates>(EnemyStates.PATROL, EnemyStates.CHASE, transition => {
                if (useLineOfSight)
                {
                    //Transition to Chase state if player becomes visible in front of stalker
                    Ray rayOfSight = new Ray();
                    rayOfSight.origin = transform.position + new Vector3(0, 4);
                    if (_facingRight)
                    {
                        rayOfSight.direction = transform.right;
                    }
                    else
                    {
                        rayOfSight.direction = transform.right * -1;
                    }
                    RaycastHit hitInfo;
                    bool isHit = Physics.Raycast(rayOfSight, out hitInfo, rangeOfSight, layerToCatch);
                    return isHit;
                } else
                {
                    return Vector3.Distance(transform.position, playerTransform.position) < rangeOfSight;
                }
            }));
            _fsm.AddTransition(new TransitionAfter<EnemyStates>(EnemyStates.CHASE, EnemyStates.PATROL, abandonChaseDuration));
            
            // CHASE <==> ATTACK
            _fsm.AddTransition(new TransitionAfter<EnemyStates>(EnemyStates.CHASE, EnemyStates.ATTACK, 0.5f, transition => Vector3.Distance(transform.position, playerTransform.position) < attackRange));
            _fsm.AddTransition(new TransitionAfter<EnemyStates>(EnemyStates.ATTACK, EnemyStates.CHASE, 0.5f, transition => Vector3.Distance(transform.position, playerTransform.position) > attackRange));

            // Testing: Transitions for directly testing states by chaning current state enum value
            //_fsm.AddTransitionFromAny(EnemyStates.WANDER, transition => currentEnemyState == EnemyStates.WANDER);
            //_fsm.AddTransitionFromAny(EnemyStates.INVESTIGATE, transition => currentEnemyState == EnemyStates.INVESTIGATE);
            //_fsm.AddTransitionFromAny(EnemyStates.PATROL, transition => currentEnemyState == EnemyStates.PATROL);
            //_fsm.AddTransitionFromAny(EnemyStates.CHASE, transition => currentEnemyState == EnemyStates.CHASE);
            //_fsm.AddTransitionFromAny(EnemyStates.ATTACK, transition => currentEnemyState == EnemyStates.ATTACK);

            // Initialize FSMs
            _fsm.Init();
        }
        private void Update()
        {
            _fsm.OnLogic();
            //print(_fsm.GetActiveHierarchyPath());
        }

        private void ChasePlayer()
        {
            if (useSplinePaths)
            {
                // Get the path direction towards the player and switch sprite to face that direction
                if (transform.position.x - playerTransform.position.x > 0)
                {
                    _direction.x = -1;
                    if (_facingRight)
                    {
                        SwitchSpriteDirection();
                    } 
                } else
                {
                    _direction.x = 1;

                    if (!_facingRight)
                    {
                        SwitchSpriteDirection();
                    }
                }

                Vector3 forward = new Vector3(1, 0, 0); // Move left and right.
                float t = 0.0f;
                if (_path != null)
                {
                    float distance =
                        SplineUtility.GetNearestPoint(_path.Splines[_currentPathIndex],
                            _path.transform.InverseTransformPoint(transform.position),
                            out float3 nearestPoint, out t);

                    if (distance > 0.01f)
                    {
                        transform.position = _path.transform.TransformPoint(nearestPoint);
                    }

                    forward = Vector3.Normalize(_path.EvaluateTangent(t));
                    forward *= _direction.x;
                }
                else
                {
                    transform.position =
                        new Vector3(transform.position.x, transform.position.y, 5.5f); // 5.5f = Room Center
                }

                _desiredVelocity = forward * Mathf.Max(chaseSpeed - _ground.Friction, 0f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, chaseSpeed * Time.deltaTime);
            }
        }
        private void Patrol()
        {
            if (_facingRight)
            {
                _direction.x = 1;
            } else
            {
                _direction.x = -1;
            }

            if (useSplinePaths)
            {
                Vector3 forward = new Vector3(1, 0, 0); // Move left and right.
                float t = 0.0f;
                if (_path != null)
                {
                    float distance =
                        SplineUtility.GetNearestPoint(_path.Splines[_currentPathIndex],
                            _path.transform.InverseTransformPoint(transform.position),
                            out float3 nearestPoint, out t);

                    if (distance > 0.01f)
                    {
                        transform.position = _path.transform.TransformPoint(nearestPoint);
                    }

                    if (_patrolOffset > patrolRange)
                    {
                        _direction.x *= -1;
                        SwitchSpriteDirection();
                        _patrolOffset = -_patrolOffset;
                    }

                    forward = Vector3.Normalize(_path.EvaluateTangent(t));
                    forward *= _direction.x;

                    _patrolOffset += 0.1f;
                }
                else
                {
                    transform.position =
                        new Vector3(transform.position.x, transform.position.y, 5.5f); // 5.5f = Room Center
                }


                _desiredVelocity = forward * Mathf.Max(patrolSpeed - _ground.Friction, 0f);
            } else
            {
                _patrolOffset += 0.1f;

                if (_patrolOffset > patrolRange)
                {
                    _direction.x *= -1;
                    SwitchSpriteDirection();
                    _patrolOffset = 0f;
                }
                Vector3 targetPosition = new Vector3(transform.position.x + patrolRange * _direction.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);
            }
            
        }

        private void FixedUpdate()
        {
            _velocity = _body.velocity;

            //_maxSpeedChange = maxAcceleration * Time.deltaTime;
            _velocity = Vector3.MoveTowards(_velocity, _desiredVelocity, chaseSpeed);

            _body.velocity = new Vector3(_velocity.x, _body.velocity.y, _velocity.z);
        }

        private void SwitchSpriteDirection()
        {
            _stalkerSprite.gameObject.transform.Rotate(0, -180, 0, Space.Self);
            _facingRight = !_facingRight;
        }
        
    }
}

