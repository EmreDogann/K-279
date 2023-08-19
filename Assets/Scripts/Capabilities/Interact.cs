using System;
using System.Collections;
using Controllers;
using Inspect;
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
        [SerializeField] private ItemInspector itemInspector;
        [SerializeField] private Inspector inspector;
        [SerializeField] private Inventory inventory;

        private Controller _controller;
        private IInteractableObjects _currentInteractable;
        private IInspectable _currentInspectable;
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
            if (!isInteractKeyDown)
            {
                return;
            }

            // Interactable
            if (_currentInteractable != null && _currentInteractable.IsInteractable())
            {
                _currentInteractable.InteractionContinues(true);
                return;
            }

            // Inspection
            if (_currentInspectable != null && _currentInspectable.IsInspectable())
            {
                _interactionActive = false;
                if (_currentInspectable.IsExpectingItem())
                {
                    ItemType expectedItem = _currentInspectable.GetExpectedItem();
                    if (inventory.ContainsItemType(expectedItem))
                    {
                        // inspector.InspectWithConfirmation(_currentInspectable, wasConfirmed =>
                        // {
                        //     if (wasConfirmed)
                        //     {
                        //         inventory.TryGetItem(expectedItem).Consume();
                        //     }
                        //
                        //     _interactionActive = true;
                        // });

                        StartCoroutine(PlaceItemAnimation(_currentInspectable, inventory.TryGetItem(expectedItem)));
                    }
                    else
                    {
                        inspector.Inspect(_currentInspectable, _ => StartCoroutine(DelayedInteractActivate()));
                    }
                }
                else
                {
                    inspector.Inspect(_currentInspectable, _ => StartCoroutine(DelayedInteractActivate()));
                }

                return;
            }

            // Item Pickup
            if (_currentItem != null)
            {
                _interactionActive = false;
                itemInspector.InspectItem(_currentItem, _controller, wasConfirmed =>
                {
                    if (wasConfirmed)
                    {
                        inventory.AddItem(_currentItem);
                        _currentItem.Pickup();
                    }

                    _currentItem = null;
                    _interactionActive = true;
                });
            }
        }

        private IEnumerator PlaceItemAnimation(IInspectable inspectable, IItem item)
        {
            _currentInspectable.GetCameraAngle().gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1.0f);
            item.Consume();
            inspectable.TryItem(item);
            yield return new WaitForSecondsRealtime(1.0f);
            _currentInspectable.GetCameraAngle().gameObject.SetActive(false);

            _interactionActive = true;
        }

        private IEnumerator DelayedInteractActivate()
        {
            yield return null;
            _interactionActive = true;
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
                _currentInteractable = interactableObject;
            }

            IInspectable inspectableObject = collision.gameObject.GetComponent<IInspectable>();
            if (inspectableObject != null)
            {
                _currentInspectable = inspectableObject;
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
                _currentInteractable = null;
            }

            IInspectable inspectableObject = collision.gameObject.GetComponent<IInspectable>();
            if (inspectableObject != null)
            {
                _currentInspectable = null;
            }

            IItem item = collision.gameObject.GetComponent<IItem>();
            if (_currentItem == item)
            {
                _currentItem = null;
            }
        }
    }
}