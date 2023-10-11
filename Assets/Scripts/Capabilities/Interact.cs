using System;
using System.Collections;
using Controllers;
using Inspect;
using Interactables;
using Items;
using Rooms;
using ScriptableObjects;
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
        private Transform _currentTransform;
        private IInteractableObjects _currentInteractable;

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

        private void OnRoomSwitching(RoomType roomType, float transitionTime, Action callback)
        {
            _interactionActive = false;
        }

        private void OnRoomActivate(RoomData roomData)
        {
            _interactionActive = true;
        }

        private void Update()
        {
            if (!_interactionActive || _currentInteractable == null || !_currentInteractable.IsInteractable())
            {
                return;
            }

            bool interactPressed = _controller.input.RetrieveInteractPress();
            if (interactPressed)
            {
                _currentInteractable.InteractionStart();
            }
            else if (_controller.input.RetrieveInteractInput())
            {
                _currentInteractable.InteractionContinues();
            }
            else if (_controller.input.RetrieveInteractRelease())
            {
                _currentInteractable.InteractionEnd();
            }
            else
            {
                return;
            }

            if (!interactPressed)
            {
                return;
            }

            bool willTakeItem = false;
            bool willUseItem = false;
            ItemInfoSO expectedItem = null;
            IItemUser itemUser = _currentTransform.gameObject.GetComponent<IItemUser>();
            if (itemUser != null)
            {
                if (itemUser.IsExpectingItem(out expectedItem))
                {
                    if (inventory.ContainsItemType(expectedItem))
                    {
                        _interactionActive = false;
                        willUseItem = true;
                    }
                }
                else if (itemUser.HasItem())
                {
                    _interactionActive = false;
                    willTakeItem = true;
                }
            }

            bool canInspectItem = false;
            IInspectable inspectableObject = _currentTransform.gameObject.GetComponent<IInspectable>();
            if (inspectableObject != null)
            {
                canInspectItem = inspectableObject.IsInspectable();
            }

            if (willUseItem && expectedItem != null)
            {
                // Use item while inspecting
                if (canInspectItem)
                {
                    StartCoroutine(PlaceItemAnimation(inspectableObject, itemUser, inventory.TryGetItem(expectedItem)));
                }
                else // Use item WITHOUT inspecting
                {
                    itemUser.TryItem(inventory.TryGetItem(expectedItem));
                    _interactionActive = true;
                }
            }

            if (!willUseItem)
            {
                // Inspect & Take item
                if (willTakeItem && canInspectItem)
                {
                    _interactionActive = false;
                    StartCoroutine(TakeItemAnimation(inspectableObject, itemUser));
                }
                else if (canInspectItem) // Regular inspection
                {
                    _interactionActive = false;
                    inspector.OpenInspect(inspectableObject, _ => StartCoroutine(DelayedInteractActivate()));
                }
            }

            IItem item = _currentTransform.gameObject.GetComponent<IItem>();
            if (item != null)
            {
                _interactionActive = false;
                itemInspector.InspectItem(item, _controller, wasConfirmed =>
                {
                    if (wasConfirmed)
                    {
                        inventory.AddItem(item);
                        item.Pickup();

                        _currentInteractable = null;
                        _currentTransform = null;
                    }

                    _interactionActive = true;
                });
            }
        }

        private IEnumerator PlaceItemAnimation(IInspectable inspectable, IItemUser itemUser, IItem item)
        {
            inspectable.GetCameraAngle().gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1.0f);
            itemUser.TryItem(item);
            yield return new WaitForSecondsRealtime(1.0f);
            inspectable.GetCameraAngle().gameObject.SetActive(false);

            _interactionActive = true;
        }

        private IEnumerator TakeItemAnimation(IInspectable inspectable, IItemUser itemUser)
        {
            inspectable.GetCameraAngle().gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1.0f);

            IItem item = itemUser.TryTakeItem();
            inventory.AddItem(item);

            yield return new WaitForSecondsRealtime(1.0f);
            inspectable.GetCameraAngle().gameObject.SetActive(false);

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
                interactableObject.InteractionAreaEnter();
                _currentInteractable = interactableObject;
                _currentTransform = collision.transform;
            }
        }

        private void OnTriggerStay(Collider collision)
        {
            if (!interactableLayerMask.Contains(collision.gameObject.layer) && _currentInteractable == null)
            {
                return;
            }

            IInteractableObjects interactableObject = collision.gameObject.GetComponent<IInteractableObjects>();
            if (interactableObject != null)
            {
                interactableObject.InteractionAreaEnter();
                _currentInteractable = interactableObject;
                _currentTransform = collision.transform;
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
                interactableObject.InteractionAreaExit();

                if (interactableObject == _currentInteractable)
                {
                    _currentInteractable = null;
                    _currentTransform = null;
                }
            }
        }
    }
}