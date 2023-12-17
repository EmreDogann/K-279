using System;
using Controllers;
using Events;
using Inspect;
using Inspect.Views;
using Interactables;
using Interaction;
using Items;
using Rooms;
using ScriptableObjects;
using ScriptableObjects.Rooms;
using UnityEngine;

namespace Capabilities
{
    [RequireComponent(typeof(Controller))]
    public class InteractionHandler : MonoBehaviour, IInteractor
    {
        [SerializeField] private ItemEventListener itemInteractedEvent;

        [SerializeField] private GuidReference pickupView_GUIDRef;
        [SerializeField] private GuidReference itemUserView_GUIDRef;

        private PickupView pickupView_cached;
        private ItemUserView itemUserView_cached;
        [SerializeField] private Inventory inventory;

        private bool _interactionActive;

        private void Awake()
        {
            _interactionActive = true;

            pickupView_GUIDRef.OnGuidRemoved += pickupView_ClearCache;
            itemUserView_GUIDRef.OnGuidRemoved += itemUserView_ClearCache;
        }

        private void pickupView_ClearCache()
        {
            pickupView_cached = null;
        }

        private void itemUserView_ClearCache()
        {
            itemUserView_cached = null;
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

                if (pickupView_cached == null && pickupView_GUIDRef.gameObject != null)
                {
                    pickupView_cached = pickupView_GUIDRef.gameObject.GetComponent<PickupView>();
                }

                if (pickupView_cached != null)
                {
                    pickupView_cached.SetupPickup(item, wasConfirmed =>
                    {
                        if (wasConfirmed)
                        {
                            inventory.AddItem(item);
                            item.Pickup();
                        }

                        _interactionActive = true;
                    });
                    ViewManager.Instance.Show(pickupView_cached);
                }
                else
                {
                    Debug.LogError(nameof(PickupView) +
                                   " not found! Aborting <color=green>[Item Pickup]</color> operation...");
                }
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

        public ItemUserInteractionType ResolveInteraction(IItemUser itemUser, ItemUserView viewOverride = null)
        {
            if (itemUserView_cached == null && itemUserView_GUIDRef.gameObject != null)
            {
                itemUserView_cached = itemUserView_GUIDRef.gameObject.GetComponent<ItemUserView>();
            }

            if (itemUserView_cached == null)
            {
                Debug.LogError(nameof(ItemUserView) +
                               " not found! Aborting <color=green>[Item Use]</color> operation...");
                return ItemUserInteractionType.Default;
            }

            ItemUserView currentView = viewOverride != null ? viewOverride : itemUserView_cached;
            if (itemUser != null)
            {
                if (itemUser.IsExpectingItem(out ItemInfoSO expectedItem) && inventory.ContainsItemType(expectedItem))
                {
                    currentView.SetupItemUserView(b =>
                    {
                        if (itemUser.IsExpectingItem(out ItemInfoSO expectedItem))
                        {
                            IItem itemToGive = inventory.TryGetItem(expectedItem);
                            if (itemToGive != null)
                            {
                                itemUser.TryItem(itemToGive);
                            }
                        }
                    }, itemUser.GetCameraAngle());
                    ViewManager.Instance.Show(itemUserView_cached);
                    return ItemUserInteractionType.GiveItem;
                }

                if (itemUser.HasItem())
                {
                    currentView.SetupItemUserView(b =>
                    {
                        IItem itemToTake = itemUser.TryTakeItem();
                        inventory.AddItem(itemToTake);
                    }, itemUser.GetCameraAngle());
                    ViewManager.Instance.Show(itemUserView_cached);
                    return ItemUserInteractionType.TakeItem;
                }
            }

            return ItemUserInteractionType.Default;
        }
    }
}