using Controllers;
using GameEntities;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

namespace Capabilities
{
    [RequireComponent(typeof(Controller))] [RequireComponent(typeof(PlayerEntity))]
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private Vector3 gunOffset = new Vector2(0.6f, 4f);
        [SerializeField] [Range(0, 100f)] private float gunRange = 10f;
        [SerializeField] [Range(0, 10f)] private float backToWalkingDelay = 3f;
        [SerializeField, Range(0, 2f)] private float walkBtnPressDelay = 0.2f;
        [SerializeField] private LayerMask layerToHit;


        private Controller _controller;

        private Ray gunRay;
        private Vector2 _direction = Vector2.zero;
        private Animator _animator;
        private Move _move;

        private bool facingRight;
        public bool gunActive;
        public bool currentlyShooting;
        public float shotCoolDownTimer;
        private int dmgPerHit;
        private float shootCoolDown;
        public float backToMovementTimer;
        public float walkBtnPressTimer;

        private readonly int _isWalking = Animator.StringToHash("IsWalking");
        private readonly int _isShooting = Animator.StringToHash("IsShooting");

        private void Awake()
        {
            _controller = GetComponent<Controller>();
            dmgPerHit = GetComponent<PlayerEntity>().GetDmg();
            shootCoolDown = GetComponent<PlayerEntity>().GetHitCoolDown();
            _animator = GetComponentInChildren<Animator>();
            _move = GetComponent<Move>();

            gunActive = true;
            facingRight = true;
            currentlyShooting = false;
            shotCoolDownTimer = shootCoolDown;
            walkBtnPressTimer = 0;
            gunRay.origin = transform.position + gunOffset;

            gunRay.direction = transform.right;
        }

        private void Update()
        {

            Debug.DrawRay(gunRay.origin, gunRay.direction * gunRange, Color.green);
            if (gunActive)
            {
                if (_controller.input.RetrieveShootInput())
                {
                    
                    // Start preparing to shoot
                    currentlyShooting = true;
                    backToMovementTimer = 0;
                    _animator.SetBool(_isShooting, true);
                    _animator.SetBool(_isWalking, false);
                    _move.StopMovement();
                    
                    
                    // Checking for gun coolDown
                    shotCoolDownTimer += Time.deltaTime;
                    if (shotCoolDownTimer <= shootCoolDown) return;
                    Debug.Log("Shooting");
                    // If cooldown over take the shot
                    gunRay.origin = gameObject.transform.position + gunOffset;
                    shotCoolDownTimer = 0;

                    RaycastHit hitInfo;
                    bool isHit = Physics.Raycast(gunRay, out hitInfo, gunRange, layerToHit);
                    if (isHit)
                    {
                        Debug.Log("Hit Enemy");
                        hitInfo.transform.gameObject.GetComponent<IEntity>()?.TakeHit(dmgPerHit);
                    }
                } else
                {
                    
                    if (currentlyShooting)
                    {

                        backToMovementTimer += Time.deltaTime;
                        // Check whether to flip player or transition to movement state

                        
                        float moveInput = _controller.input.RetrieveMoveInput(gameObject);
                        if (moveInput != 0)
                        {
                            StartCoroutine(WalkButtonDelay(moveInput));
                        } else
                        {
                            StopCoroutine(WalkButtonDelay(moveInput));
                            walkBtnPressTimer = 0;
                        }

                        // Wait a set amount of time before returning to idle

                        
                        
                        if (backToMovementTimer >= backToWalkingDelay)
                        {
                            
                            currentlyShooting = false;
                            // Go back to Idle
                            _move.RestartMovement();
                            backToMovementTimer = 0;
                            shotCoolDownTimer = 0;
                            _animator.SetBool(_isWalking, false);
                            _animator.SetBool(_isShooting, false);
                        }
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            _direction.x = _controller.input.RetrieveMoveInput(gameObject);
            if (_direction.x > 0f && !facingRight)
            {
                _move.FlipPlayer();
                FlipGun();
            }
            else if (_direction.x < 0f && facingRight)
            {
                _move.FlipPlayer();
                FlipGun();
            }
        }

        private void FlipGun()
        {
            gunOffset.x *= -1;

            if (facingRight)
            {
                gunRay.direction = gameObject.transform.right * -1;
            }
            else
            {
                gunRay.direction = gameObject.transform.right;
            }

            facingRight = !facingRight;
        }

        private IEnumerator WalkButtonDelay(float moveInput)
        {
            while (moveInput != 0 && walkBtnPressTimer < walkBtnPressDelay)
            {
                walkBtnPressTimer += Time.deltaTime;
                if (walkBtnPressTimer > walkBtnPressDelay) {
                    _move.RestartMovement();
                    currentlyShooting = false;
                    _animator.SetBool(_isWalking, true);
                    _animator.SetBool(_isShooting, false);
                    walkBtnPressTimer = 0;
                    shotCoolDownTimer = 0;
                    yield break;
                }
                moveInput = _controller.input.RetrieveMoveInput(gameObject);
                yield return null;
            }

            
        }
    }
    
}