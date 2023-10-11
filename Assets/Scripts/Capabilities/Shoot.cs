using System.Collections;
using Audio;
using Cinemachine;
using Controllers;
using DG.Tweening;
using Events;
using GameEntities;
using Items;
using MyBox;
using ScriptableObjects;
using UnityEngine;

namespace Capabilities
{
    [RequireComponent(typeof(Controller))] [RequireComponent(typeof(PlayerEntity))]
    public class Shoot : MonoBehaviour
    {
        [Separator("Settings")]
        [SerializeField] private Vector3 gunOffset = new Vector2(0.6f, 4f);
        [SerializeField] [Range(0, 100f)] private float gunRange = 10f;
        [SerializeField] [Range(0, 10f)] private float backToWalkingDelay = 3f;
        [SerializeField] [Range(0, 2f)] private float walkBtnPressDelay = 0.2f;
        [SerializeField] private int maxAmmoCount = 5;
        [SerializeField] private LayerMask layerToHit;
        [SerializeField] private ItemInfoSO ammoItemInfo;

        [Separator("Effects")]
        [SerializeField] private Light muzzleFlash;
        [SerializeField] private float muzzleFlashLifetime;
        [SerializeField] private AudioSO gunshotAudio;
        [SerializeField] private AudioSO lowAmmoAudio;
        [SerializeField] private AudioSO reloadAudio;
        [SerializeField] private bool shakeCameraOnGunshot;
        [ConditionalField(nameof(shakeCameraOnGunshot))]
        [SerializeField] private CinemachineImpulseListener impulseListener;
        [ConditionalField(nameof(shakeCameraOnGunshot))]
        [SerializeField] private CinemachineImpulseSource impulseSource;
        [ConditionalField(nameof(shakeCameraOnGunshot))] [SerializeField] private ScreenShakeProfile screenShakeProfile;

        [Separator("Events")]
        [SerializeField] private BoolEventListener pauseEvent;

        private Sequence _muzzleFlashSequence;
        private PlayerEntity _playerEntity;
        private Controller _controller;

        private Ray _gunRay;
        private Vector2 _direction = Vector2.zero;
        private Animator _animator;
        private Move _move;
        private Inventory _inventory;

        private bool _isPaused;
        private bool _facingRight;
        private bool _currentlyShooting;

        private int _dmgPerHit;
        private int _currentAmmoCount;
        private float _shotCoolDownTimer;
        private float _shootCoolDown;
        private float _backToMovementTimer;
        private float _walkBtnPressTimer;

        private bool _isShootQueued;

        private Coroutine _waitForAnimCoroutine;
        private Coroutine _walkBtnDelayCoroutine;

        private readonly int _isWalking = Animator.StringToHash("IsWalking");
        private readonly int _isShooting = Animator.StringToHash("IsShooting");

        private void Awake()
        {
            _controller = GetComponent<Controller>();
            _playerEntity = GetComponent<PlayerEntity>();
            _animator = GetComponentInChildren<Animator>();
            _move = GetComponent<Move>();
            _inventory = GetComponent<Inventory>();

            _dmgPerHit = _playerEntity.GetDmg();
            _shootCoolDown = _playerEntity.GetHitCoolDown();

            _facingRight = true;
            _currentlyShooting = false;
            _currentAmmoCount = maxAmmoCount;
            _shotCoolDownTimer = _shootCoolDown;
            _walkBtnPressTimer = 0;
            _gunRay.origin = transform.position + gunOffset;

            _gunRay.direction = transform.right;

            muzzleFlash.gameObject.SetActive(false);

            _muzzleFlashSequence = DOTween.Sequence();
            _muzzleFlashSequence
                .AppendCallback(() =>
                {
                    Vector3 position = transform.position + gunOffset;
                    position.z = muzzleFlash.transform.position.z;
                    muzzleFlash.transform.position = position;
                    muzzleFlash.gameObject.SetActive(true);
                })
                .AppendInterval(muzzleFlashLifetime)
                .AppendCallback(() => muzzleFlash.gameObject.SetActive(false))
                .SetAutoKill(false)
                .Pause();
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

            if (_controller.input.RetrieveReloadInput() && _currentAmmoCount < maxAmmoCount)
            {
                // Check if reload is possible
                IItem item = _inventory.TryGetItem(ammoItemInfo);
                if (item != null)
                {
                    // If possible do the reload
                    _currentAmmoCount = maxAmmoCount;
                    item.Consume();
                    reloadAudio.Play2D();
                }
            }

            // Debug.DrawRay(_gunRay.origin, _gunRay.direction * gunRange, Color.green);
            if (_controller.input.RetrieveShootInput())
            {
                // Start preparing to shoot
                _currentlyShooting = true;
                _backToMovementTimer = 0;
                _walkBtnPressTimer = 0;
                _move.StopMovement();

                if (_walkBtnDelayCoroutine != null)
                {
                    StopCoroutine(_walkBtnDelayCoroutine);
                    _walkBtnDelayCoroutine = null;
                }

                // Checking for gun coolDown
                _shotCoolDownTimer += Time.deltaTime;
                if (_shotCoolDownTimer <= _shootCoolDown)
                {
                    if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot") &&
                        _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        _isShootQueued = true;
                    }

                    return;
                }

                _shotCoolDownTimer = 0;

                if (!_animator.GetBool(_isShooting))
                {
                    _animator.SetBool(_isShooting, true);
                    _animator.SetBool(_isWalking, false);

                    if (_waitForAnimCoroutine != null)
                    {
                        StopCoroutine(_waitForAnimCoroutine);
                        _waitForAnimCoroutine = null;
                    }

                    _waitForAnimCoroutine = StartCoroutine(WaitForAnim(_animator, "IdleToShoot"));
                }
                else
                {
                    if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
                    {
                        _animator.Play(0);
                    }

                    FireGunshot();
                }
            }
            else
            {
                if (!_currentlyShooting)
                {
                    return;
                }

                // Check whether to flip player or transition to movement state
                _backToMovementTimer += Time.deltaTime;

                _shotCoolDownTimer += Time.deltaTime;
                if (_shotCoolDownTimer > _shootCoolDown)
                {
                    if (_isShootQueued)
                    {
                        _isShootQueued = false;

                        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
                        {
                            _animator.Play(0);
                        }

                        FireGunshot();
                        _shotCoolDownTimer = 0;
                    }
                }

                // if (_shotCoolDownTimer <= _shootCoolDown * 0.8f)
                // {
                //     return;
                // }

                float moveInput = _controller.input.RetrieveMoveInput(gameObject);
                if (moveInput != 0)
                {
                    if (_walkBtnDelayCoroutine == null)
                    {
                        _walkBtnDelayCoroutine = StartCoroutine(WalkButtonDelay(moveInput));
                        _walkBtnPressTimer = 0;
                    }
                }
                else
                {
                    if (_walkBtnDelayCoroutine != null)
                    {
                        StopCoroutine(_walkBtnDelayCoroutine);
                        _walkBtnDelayCoroutine = null;
                    }
                }

                // Wait a set amount of time before returning to idle
                if (_backToMovementTimer >= backToWalkingDelay)
                {
                    _currentlyShooting = false;
                    // Go back to Idle
                    _move.RestartMovement();
                    _backToMovementTimer = 0;
                    _shotCoolDownTimer = _shootCoolDown;
                    _animator.SetBool(_isWalking, false);
                    _animator.SetBool(_isShooting, false);
                }
            }
        }

        private void ReloadGun() {}

        private void FireGunshot()
        {
            if (_currentAmmoCount <= 0)
            {
                lowAmmoAudio.Play2D();
                return;
            }

            if (_currentAmmoCount == 1)
            {
                lowAmmoAudio.Play2D();
            }

            _currentAmmoCount -= 1;
            _shotCoolDownTimer = 0;

            _gunRay.origin = gameObject.transform.position + gunOffset;
            RaycastHit hitInfo;
            bool isHit = Physics.Raycast(_gunRay, out hitInfo, gunRange, layerToHit);
            if (isHit)
            {
                hitInfo.transform.gameObject.GetComponent<IEntity>()?.TakeHit(_dmgPerHit);
            }

            _muzzleFlashSequence.Restart();
            gunshotAudio.Play2D();


            if (shakeCameraOnGunshot && screenShakeProfile && impulseSource)
            {
                impulseSource.m_ImpulseDefinition.m_ImpulseDuration = screenShakeProfile.impactTime;
                impulseSource.m_DefaultVelocity = screenShakeProfile.defaultVelocity;
                impulseListener.m_ReactionSettings.m_AmplitudeGain = screenShakeProfile.listenerAmplitude;
                impulseListener.m_ReactionSettings.m_FrequencyGain = screenShakeProfile.listenerFrequency;
                impulseListener.m_ReactionSettings.m_Duration = screenShakeProfile.listenerDuration;

                impulseSource.GenerateImpulseAtPositionWithVelocity(transform.position,
                    screenShakeProfile.defaultVelocity * screenShakeProfile.impactForce);
            }
        }

        private void FixedUpdate()
        {
            _direction.x = _controller.input.RetrieveMoveInput(gameObject);
            if (_direction.x > 0f && !_facingRight)
            {
                _move.FlipPlayer();
                FlipGun();
            }
            else if (_direction.x < 0f && _facingRight)
            {
                _move.FlipPlayer();
                FlipGun();
            }
        }

        private void FlipGun()
        {
            gunOffset.x *= -1;

            if (_facingRight)
            {
                _gunRay.direction = gameObject.transform.right * -1;
            }
            else
            {
                _gunRay.direction = gameObject.transform.right;
            }

            _facingRight = !_facingRight;
        }

        private IEnumerator WalkButtonDelay(float moveInput)
        {
            while (moveInput != 0 && _walkBtnPressTimer < walkBtnPressDelay)
            {
                _walkBtnPressTimer += Time.deltaTime;
                if (_walkBtnPressTimer > walkBtnPressDelay)
                {
                    _move.RestartMovement();
                    _currentlyShooting = false;
                    _animator.SetBool(_isWalking, true);
                    _animator.SetBool(_isShooting, false);
                    _walkBtnPressTimer = 0;
                    _shotCoolDownTimer = _shootCoolDown;
                    yield break;
                }

                moveInput = _controller.input.RetrieveMoveInput(gameObject);
                yield return null;
            }
        }

        public IEnumerator WaitForAnim(Animator targetAnim, string stateName)
        {
            //Wait until we enter the current state
            while (!targetAnim.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            {
                yield return null;
            }

            //Now, Wait until the current state is done playing
            while (targetAnim.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            {
                yield return null;
            }

            //Done playing!
            FireGunshot();
            _shotCoolDownTimer = 0;
        }
    }
}