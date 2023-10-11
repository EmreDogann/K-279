using System;
using Cinemachine;
using Inspect;
using Interactables;
using Items;
using MyBox;
using ScriptableObjects;
using UnityEngine;

namespace ScriptedEvents.EventTriggers
{
    public class ItemInteractionEventTrigger : MonoBehaviour, IEventTrigger, IInteractableObjects, IInspectable
    {
        [Separator("Optional Values")]
        [SerializeField] private ItemInfoSO expectedItem;
        [SerializeField] private CinemachineVirtualCamera inspectVirtualCamera;
        [SerializeField] private string inspectMessage;

        private bool _isTriggered;
        private bool _isInspectable = true;
        private bool _setIsTriggered; // To be set the next frame, so inspection can work without race conditions.

        public event EventHandler EventTriggered;

        private void OnValidate()
        {
            _isInspectable = true;
        }

        public bool IsTriggered()
        {
            return _isTriggered;
        }

        public void InteractionStart() {}

        public void InteractionContinues() {}

        public void InteractionEnd() {}

        public void InteractionAreaEnter() {}
        public void InteractionAreaExit() {}

        public bool IsInteractable()
        {
            return true;
        }

        private void OnEventTriggered()
        {
            EventTriggered?.Invoke(this, EventArgs.Empty);
        }

        public CinemachineVirtualCamera GetCameraAngle()
        {
            return inspectVirtualCamera;
        }

        public string GetMessage()
        {
            return inspectMessage;
        }

        public bool IsInspectable()
        {
            return _isInspectable && !_isTriggered;
        }

        public bool IsExpectingItem(out ItemInfoSO item)
        {
            item = expectedItem;
            return !_isTriggered;
        }

        public bool ShouldPlayInspectAnimation()
        {
            return false;
        }

        public bool HasAvailableItem()
        {
            return false;
        }

        public bool TryItem(IItem item)
        {
            bool isExpectingItem = IsExpectingItem(out ItemInfoSO itemInfo);
            if (isExpectingItem && item.GetItemInfo() == itemInfo)
            {
                item.Consume();
                _isTriggered = true;
                _isInspectable = false;
                OnEventTriggered();

                return true;
            }

            return false;
        }

        public IItem TryTakeItem()
        {
            return null;
        }
    }
}