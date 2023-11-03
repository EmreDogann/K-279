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

        [SerializeField] private bool blockedByUI;
        [SerializeField] private LayerMask mask;

        private InteractableBase _hoverTarget;
        private InteractableBase _activeTarget;
        private RaycastHit2D _hit;
        private Camera _mainCamera;

        private bool _isPaused;
        private BoolEventListener _onGamePausedEvent;

        public static Action<InteractableBase> OnInteract;
        public static Action<InteractableBase> OnHover;
        public static Action<InteractableBase> OnAltInteract;

        private void Awake()
        {
            _mainCamera = Camera.main;
            cursorGraphic.SetInteractionRange(interactionRange);

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
            CheckForInteraction();
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
                if (inputHandler.RetrieveInteractPress())
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
            _hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()),
                Vector2.zero, Mathf.Infinity, mask);
            InteractableBase newTarget = null;

            if (_hit.collider && !IsBlockedByUI())
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