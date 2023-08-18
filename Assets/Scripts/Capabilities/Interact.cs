using System;
using System.Collections.Generic;
using Controllers;
using Interactables;
using Items;
using Rooms;
using UnityEngine;
using Utils;

namespace Capabilities
{
    [RequireComponent(typeof(Controller))]
    public class Interact : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayerMask;
        [SerializeField] private ItemInspector _itemInspector;

        private Controller _controller;
        private readonly List<IInteractableObjects> _currentInteractables = new List<IInteractableObjects>();
        private IItem _currentItem;

        private bool _interactionActive;

        private void Awake()
        {
            _controller = GetComponent<Controller>();
            _interactionActive = true;
        }

        private void OnEnable()
        {
            Door.OnRoomSwitching += OnRoomSwitching;
            Room.OnRoomActivate += OnRoomActivate;
        }

        private void OnDisable()
        {
            Door.OnRoomSwitching -= OnRoomSwitching;
            Room.OnRoomActivate -= OnRoomActivate;
        }

        private void OnRoomSwitching(RoomType roomType, Action callback)
        {
            _interactionActive = false;
        }

        private void OnRoomActivate(RoomData roomData)
        {
            _interactionActive = true;
        }

        private void Update()
        {
            if (!_interactionActive)
            {
                return;
            }

            bool isInteractKeyDown = _controller.input.RetrieveInteractInput();
            if (isInteractKeyDown)
            {
                foreach (IInteractableObjects interactable in _currentInteractables)
                {
                    interactable.InteractionContinues(true);
                }
            }

            if (_controller.input.RetrieveInteractInput() && _currentItem != null)
            {
                _interactionActive = false;
                _itemInspector.InspectItem(_currentItem, wasConfirmed =>
                {
                    if (wasConfirmed)
                    {
                        // TODO: Add inventory item.
                        Debug.Log("Add item: " + _currentItem.GetItemInfo().itemName);
                    }

                    _currentItem = null;
                    _interactionActive = true;
                });
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (!interactableLayerMask.Contains(collision.gameObject.layer))
            {
                return;
            }

            IInteractableObjects interactableObject = collision.gameObject.GetComponent<IInteractableObjects>();
            if (interactableObject != null)
            {
                interactableObject.InteractionStart();
                _currentInteractables.Add(interactableObject);
                return;
            }

            IItem item = collision.gameObject.GetComponent<IItem>();
            if (_currentItem == null && item != null)
            {
                _currentItem = item;
            }
        }

        private void OnTriggerStay(Collider collision)
        {
            if (!interactableLayerMask.Contains(collision.gameObject.layer))
            {
                return;
            }

            if (_currentItem == null)
            {
                IItem item = collision.gameObject.GetComponent<IItem>();
                if (item != null)
                {
                    _currentItem = item;
                }
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (!interactableLayerMask.Contains(collision.gameObject.layer))
            {
                return;
            }

            IInteractableObjects interactableObject = collision.gameObject.GetComponent<IInteractableObjects>();
            if (interactableObject != null)
            {
                interactableObject.InteractionEnd();
                _currentInteractables.Remove(interactableObject);
                return;
            }

            IItem item = collision.gameObject.GetComponent<IItem>();
            if (_currentItem == item)
            {
                _currentItem = null;
            }
        }
    }
}