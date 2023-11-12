using System;
using Controllers;
using Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Interaction
{
    public class MouseInteraction : MonoBehaviour
    {
        [SerializeField] private InputController inputHandler;
        [SerializeField] private MouseCursorGraphic cursorGraphic;

        [Tooltip("The distance from the character that the player can interact with in world space units.")]
        [SerializeField] private float interactionRange;
        [SerializeField] private Collider interacterCollider;

        [SerializeField] private bool blockedByUI;
        [SerializeField] private LayerMask mask;

        private InteractableBase _hoverTarget;
        private InteractableBase _activeTarget;
        private RaycastHit _hit;
        private Camera _mainCamera;
        private bool _isInRange = true;

        private bool _isPaused;
        private BoolEventListener _onGamePausedEvent;

        public static Action<InteractableBase> OnInteract;
        public static Action<InteractableBase> OnHover;
        public static Action<InteractableBase> OnAltInteract;

        private void Awake()
        {
            _mainCamera = Camera.main;

            _onGamePausedEvent = GetComponent<BoolEventListener>();
        }

        private void OnEnable()
        {
            _onGamePausedEvent.Response.AddListener(OnGamePause);
        }

        private void OnDisable()
        {
            _onGamePausedEvent.Response.RemoveListener(OnGamePause);
        }

        private void Update()
        {
            if (!_isPaused && IsInInteractionRange())
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
                cursorGraphic.SetInteractState(false);
                _isInRange = false;
            }
            else if (distance < interactionRange && !_isInRange)
            {
                cursorGraphic.SetInteractState(true);
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

                _activeTarget.OnInteract();
            }
            else
            {
                if (inputHandler.IsInteractPressed())
                {
                    _activeTarget = _hoverTarget;
                    _activeTarget?.OnStartInteract();

                    OnInteract?.Invoke(_activeTarget);

                    return _activeTarget;
                }
            }

            return _activeTarget;
        }

        private void StopInteraction()
        {
            _activeTarget.OnEndInteract();
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
                _hoverTarget.OnEndHover();
            }

            if (newTarget != null && newTarget.IsHoverable)
            {
                newTarget.OnStartHover();
            }

            OnHover?.Invoke(newTarget);
            _hoverTarget = newTarget;
        }

        private void OnGamePause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        private bool IsBlockedByUI()
        {
            return blockedByUI && EventSystem.current.IsPointerOverGameObject();
        }
    }
}