using System;
using AYellowpaper;
using Controllers;
using Events;
using MyBox;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Interaction
{
    public class MouseInteraction : MonoBehaviour
    {
        [SerializeField] private InputController inputHandler;
        [SerializeField] private InterfaceReference<IInteractor, MonoBehaviour> interactionHandler;

        [Tooltip("The distance from the character that the player can interact with in world space units.")]
        [SerializeField] private float interactionRange;
        [SerializeField] private Collider interacterCollider;

        [SerializeField] private bool blockedByUI;
        [SerializeField] private LayerMask mask;

        [SerializeField] private BoolEventListener _gamePausedEvent;
        [SerializeField] private BoolEventListener _interactionBlockerEvent;

        private InteractableBase _hoverTarget;
        private InteractableBase _activeTarget;
        private RaycastHit _hit;
        private Camera _mainCamera;
        private bool _isInRange = true;

        private bool _isPaused;
        [ReadOnly] [SerializeField] private bool _isInteractionBlocked;

        public static Action<InteractableBase> OnInteract;
        public static Action<InteractableBase> OnHover;
        public static Action<InteractableBase> OnAltInteract;
        public static Action<bool> OnInteractionStateChange;

        private void Awake()
        {
            _mainCamera = Camera.main;

            if (interactionHandler.Value == null)
            {
                interactionHandler.Value = GetComponent<IInteractor>();
            }
        }

        private void Reset()
        {
            interactionHandler.Value = GetComponent<IInteractor>();
        }

        private void OnEnable()
        {
            _gamePausedEvent.Response.AddListener(OnGamePause);
            _interactionBlockerEvent.Response.AddListener(OnInteractionBlocker);
        }

        private void OnDisable()
        {
            _gamePausedEvent.Response.RemoveListener(OnGamePause);
            _interactionBlockerEvent.Response.RemoveListener(OnInteractionBlocker);
        }

        private void OnGamePause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        private void OnInteractionBlocker(bool shouldBlockInteractions)
        {
            _isInteractionBlocked = shouldBlockInteractions;
        }

        private bool IsBlockedByUI()
        {
            return blockedByUI && EventSystem.current.IsPointerOverGameObject();
        }

        private void Update()
        {
            if (!_isPaused && IsInInteractionRange() && !_isInteractionBlocked)
            {
                CheckForInteraction();
            }
        }

        private bool IsInInteractionRange()
        {
            Vector3 mousePosSS = Mouse.current.position.value;
            mousePosSS.z = Mathf.Abs(_mainCamera.transform.position.z - interacterCollider.transform.position.z);

            Vector3 cursorPos = _mainCamera.ScreenToWorldPoint(mousePosSS);
            Vector2 playerPos = interacterCollider.ClosestPointOnBounds(cursorPos);
            float distance = Vector2.Distance(playerPos, cursorPos);

            if (distance > interactionRange && _isInRange)
            {
                OnInteractionStateChange?.Invoke(false);
                _isInRange = false;
            }
            else if (distance < interactionRange && !_isInRange)
            {
                OnInteractionStateChange?.Invoke(true);
                _isInRange = true;
            }


            return _isInRange;
        }

        public InteractableBase CheckForInteraction()
        {
            if (!_activeTarget)
            {
                RaycastForInteractable();
            }

            // if (!_hoverTarget || !_hoverTarget.IsInteractable)
            // {
            //     return _activeTarget;
            // }

            if (_activeTarget)
            {
                if (!_activeTarget.HoldInteract || _isPaused)
                {
                    StopInteraction();
                    return null;
                }

                if (_activeTarget.IsHoldInteractFinished())
                {
                    StopInteraction();
                    return null;
                }

                _activeTarget.OnInteract(interactionHandler.Value);
            }
            else
            {
                if (inputHandler.IsInteractPressed())
                {
                    _activeTarget = _hoverTarget;
                    _activeTarget?.OnStartInteract(interactionHandler.Value);

                    OnInteract?.Invoke(_activeTarget);

                    return _activeTarget;
                }
            }

            return _activeTarget;
        }

        private void StopInteraction()
        {
            _activeTarget.OnEndInteract(interactionHandler.Value);
            _activeTarget = null;
        }

        private void RaycastForInteractable()
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
                if (_mainCamera == null)
                {
                    return;
                }
            }

            // Send ray out from cursor position.
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = _mainCamera.nearClipPlane;
            bool isHit = Physics.Raycast(_mainCamera.ScreenPointToRay(mousePos), out _hit, Mathf.Infinity, mask);
            InteractableBase newTarget = null;

            if (isHit && !IsBlockedByUI())
            {
                // Will use the last InteractableBase found on the GameObject which is active.
                var interactables = _hit.collider.GetComponents<InteractableBase>();
                foreach (InteractableBase interactable in interactables)
                {
                    if (interactable != null && (interactable.IsInteractable || interactable.IsHoverable))
                    {
                        newTarget = interactable;
                    }
                }
            }

            if (newTarget == _hoverTarget)
            {
                return;
            }

            if (_hoverTarget != null && _hoverTarget.IsHoverable)
            {
                _hoverTarget.OnEndHover(interactionHandler.Value);
            }

            if (newTarget != null && newTarget.IsHoverable)
            {
                newTarget.OnStartHover(interactionHandler.Value);
            }

            OnHover?.Invoke(newTarget);
            _hoverTarget = newTarget;
        }
    }
}