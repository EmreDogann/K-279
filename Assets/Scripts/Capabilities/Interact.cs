using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Events;
using Inspect;
using Inspect.Views;
using Interactables;
using Items;
using Rooms;
using UnityEngine;

namespace Capabilities
{
    [RequireComponent(typeof(Controller))]
    public class Interact : MonoBehaviour
    {
        private class InteractableData
        {
            public IInteractableObjects Interactable;
            public Transform Transform;
        }

        [SerializeField] private bool globalInteractActive = true;

        [SerializeField] private LayerMask interactableLayerMask;
        [SerializeField] private ItemEventListener itemInteractedEvent;

        [SerializeField] private PickupView pickupView;
        [SerializeField] private Inventory inventory;

        private Controller _controller;
        private readonly List<InteractableData> _currentInteractables = new List<InteractableData>();

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

            itemInteractedEvent.Response.AddListener(OnItemInteract);
        }

        private void OnDisable()
        {
            Door.OnRoomSwitching -= OnRoomSwitching;
            Room.OnRoomActivate -= OnRoomActivate;

            itemInteractedEvent.Response.RemoveListener(OnItemInteract);
        }

        private void OnItemInteract(IItem item)
        {
            if (item != null)
            {
                _interactionActive = false;
                pickupView.SetupPickup(item, wasConfirmed =>
                {
                    if (wasConfirmed)
                    {
                        inventory.AddItem(item);
                        item.Pickup();
                    }

                    _interactionActive = true;
                });
                ViewManager.Instance.Show(pickupView);
            }
        }

        private void OnRoomSwitching(RoomType roomType, float transitionTime, Action callback)
        {
            _interactionActive = false;
        }

        private void OnRoomActivate(RoomData roomData)
        {
            _interactionActive = true;
        }

//         private void Update()
//         {
//             if (!globalInteractActive || !_interactionActive || _currentInteractables.Count <= 0)
//             {
//                 return;
//             }
//
//             foreach (InteractableData data in _currentInteractables)
//             {
//                 if (!data.Interactable.IsInteractable())
//                 {
//                     continue;
//                 }
//
//                 bool interactPressed = _controller.input.IsInteractPressed();
//                 if (interactPressed)
//                 {
//                     data.Interactable.InteractionStart();
//                 }
//                 else if (_controller.input.GetInteractInput())
//                 {
//                     data.Interactable.InteractionContinues();
//                 }
//                 else if (_controller.input.IsInteractReleased())
//                 {
//                     data.Interactable.InteractionEnd();
//                 }
//                 else
//                 {
//                     continue;
//                 }
//
//                 if (!interactPressed)
//                 {
//                     continue;
//                 }
//
//                 bool willTakeItem = false;
//                 bool willUseItem = false;
//                 ItemInfoSO expectedItem = null;
//                 IItemUser itemUser = data.Transform.GetComponent<IItemUser>();
//                 if (itemUser != null)
//                 {
//                     if (itemUser.IsExpectingItem(out expectedItem))
//                     {
//                         if (inventory.ContainsItemType(expectedItem))
//                         {
//                             _interactionActive = false;
//                             willUseItem = true;
//                         }
//                     }
//                     else if (itemUser.HasItem())
//                     {
//                         _interactionActive = false;
//                         willTakeItem = true;
//                     }
//                 }
//
//                 bool canInspectItem = false;
//                 IInspectable inspectableObject = data.Transform.GetComponent<IInspectable>();
//                 if (inspectableObject != null)
//                 {
//                     canInspectItem = inspectableObject.IsInspectable();
//                 }
//
//                 if (willUseItem && expectedItem != null)
//                 {
//                     // Use item while inspecting
//                     if (canInspectItem)
//                     {
//                         StartCoroutine(PlaceItemAnimation(inspectableObject, itemUser,
//                             inventory.TryGetItem(expectedItem)));
//                     }
//                     else // Use item WITHOUT inspecting
//                     {
//                         itemUser.TryItem(inventory.TryGetItem(expectedItem));
//                         _interactionActive = true;
//                     }
//                 }
//
//                 if (!willUseItem)
//                 {
//                     // Inspect & Take item
//                     if (willTakeItem && canInspectItem)
//                     {
//                         _interactionActive = false;
//                         StartCoroutine(TakeItemAnimation(inspectableObject, itemUser));
//                     }
//                     else if (canInspectItem) // Regular inspection
//                     {
//                         _interactionActive = false;
//                         // inspector.OpenInspect(inspectableObject, _ => StartCoroutine(DelayedInteractActivate()));
//                     }
//                 }
//
//                 IItem item = data.Transform.GetComponent<IItem>();
//                 if (item != null)
//                 {
//                     // _interactionActive = false;
//                     /*itemInspector.InspectItem(item, _controller, wasConfirmed =>
//                     {
//                         if (wasConfirmed)
//                         {
//                             inventory.AddItem(item);
//                             item.Pickup();
//
//                             _currentInteractables.Remove(data);
//                         }
//
//                         _interactionActive = true;
//                     });*/
//                 }
//             }
//         }

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

        // private void OnTriggerEnter(Collider collision)
        // {
        //     if (!globalInteractActive || !interactableLayerMask.Contains(collision.gameObject.layer))
        //     {
        //         return;
        //     }
        //
        //     var interactableObjects = collision.gameObject.GetComponents<IInteractableObjects>();
        //     foreach (IInteractableObjects interactableObject in interactableObjects)
        //     {
        //         if (interactableObject == null)
        //         {
        //             continue;
        //         }
        //
        //         interactableObject.InteractionAreaEnter();
        //
        //         if (!_currentInteractables.Exists(x => x.Interactable == interactableObject))
        //         {
        //             _currentInteractables.Add(new InteractableData
        //                 { Interactable = interactableObject, Transform = collision.transform });
        //         }
        //     }
        // }

        // private void OnTriggerStay(Collider collision)
        // {
        //     if (!globalInteractActive || !interactableLayerMask.Contains(collision.gameObject.layer))
        //     {
        //         return;
        //     }
        //
        //     IInteractableObjects interactableObject = collision.gameObject.GetComponent<IInteractableObjects>();
        //     if (interactableObject != null && !_currentInteractables.Contains(interactableObject))
        //     {
        //         interactableObject.InteractionAreaEnter();
        //         _currentInteractables.Add(interactableObject);
        //         _currentTransform = collision.transform;
        //     }
        // }

        // private void OnTriggerExit(Collider collision)
        // {
        //     if (!globalInteractActive || !interactableLayerMask.Contains(collision.gameObject.layer))
        //     {
        //         return;
        //     }
        //
        //     var interactableObjects = collision.gameObject.GetComponents<IInteractableObjects>();
        //     foreach (IInteractableObjects interactableObject in interactableObjects)
        //     {
        //         if (interactableObject == null)
        //         {
        //             continue;
        //         }
        //
        //         interactableObject.InteractionAreaExit();
        //
        //         _currentInteractables.RemoveAll(x => x.Interactable == interactableObject);
        //     }
        // }
    }
}