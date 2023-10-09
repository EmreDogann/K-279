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
            if (!_interactionActive || _currentInteractable == null || !_currentInteractable.IsInteractable())
            {
                return;
            }

            if (_controller.input.RetrieveInteractPress())
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

            IInspectable inspectableObject = _currentTransform.gameObject.GetComponent<IInspectable>();
            if (inspectableObject != null)
            {
                if (inspectableObject.IsExpectingItem(out ItemType expectedItem))
                {
                    if (inventory.ContainsItemType(expectedItem))
                    {
                        _interactionActive = false;
                        StartCoroutine(PlaceItemAnimation(inspectableObject, inventory.TryGetItem(expectedItem)));
                    }
                    else if (inspectableObject.IsInspectable())
                    {
                        _interactionActive = false;
                        inspector.OpenInspect(inspectableObject, _ => StartCoroutine(DelayedInteractActivate()));
                    }
                }
                else
                {
                    if (inspectableObject.HasAvailableItem())
                    {
                        _interactionActive = false;
                        StartCoroutine(TakeItemAnimation(inspectableObject));
                    }
                    else if (inspectableObject.IsInspectable())
                    {
                        _interactionActive = false;
                        inspector.OpenInspect(inspectableObject, _ => StartCoroutine(DelayedInteractActivate()));
                    }
                }

                return;
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

        private IEnumerator PlaceItemAnimation(IInspectable inspectable, IItem item)
        {
            inspectable.GetCameraAngle().gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1.0f);
            inspectable.TryItem(item);
            yield return new WaitForSecondsRealtime(1.0f);
            inspectable.GetCameraAngle().gameObject.SetActive(false);

            _interactionActive = true;
        }

        private IEnumerator TakeItemAnimation(IInspectable inspectable)
        {
            inspectable.GetCameraAngle().gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1.0f);

            IItem item = inspectable.TryTakeItem();
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