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

        [SerializeField] private PickupView pickupView;
        [SerializeField] private ItemUserView itemUserView;
        [SerializeField] private Inventory inventory;

        private bool _interactionActive;

        private void Awake()
        {
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

        public ItemUserInteractionType ResolveInteraction(IItemUser itemUser, ItemUserView viewOverride = null)
        {
            ItemUserView currentView = viewOverride != null ? viewOverride : itemUserView;
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
                    ViewManager.Instance.Show(itemUserView);
                    return ItemUserInteractionType.GiveItem;
                }

                if (itemUser.HasItem())
                {
                    currentView.SetupItemUserView(b =>
                    {
                        IItem itemToTake = itemUser.TryTakeItem();
                        inventory.AddItem(itemToTake);
                    }, itemUser.GetCameraAngle());
                    ViewManager.Instance.Show(itemUserView);
                    return ItemUserInteractionType.TakeItem;
                }
            }

            return ItemUserInteractionType.Default;
        }
    }
}