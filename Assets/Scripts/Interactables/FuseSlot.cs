using System;
using Cinemachine;
using Inspect;
using Items;
using MyBox;
using ScriptableObjects;
using UnityEngine;

namespace Interactables
{
    public class FuseSlot : MonoBehaviour, IInteractableObjects, IInspectable, IItemUser
    {
        [field: Separator("Interaction")]
        [field: SerializeField] public bool Interactable { get; set; } = true;
        [SerializeField] private Transform fuseInPlaceMesh;

        [Separator("Inspection")]
        [SerializeField] private bool isInspectable;
        [SerializeField] private ItemInfoSO expectedItem;

        [SerializeField] private CinemachineVirtualCamera inspectVirtualCamera;
        [SerializeField] private string inspectMessage;

        public static event Action<bool> PowerChanged;

        private IItem _fuseItem;

        private void Awake()
        {
            inspectVirtualCamera.gameObject.SetActive(false);

            Transform firstChild = transform.GetChild(0);
            if (firstChild != null)
            {
                _fuseItem = firstChild.GetComponent<IItem>();
            }
        }

        public void InteractionContinues() {}

        public void InteractionStart() {}
        public void InteractionEnd() {}
        public void InteractionAreaEnter() {}
        public void InteractionAreaExit() {}

        public bool IsInteractable()
        {
            return Interactable;
        }

        public CinemachineVirtualCamera GetCameraAngle()
        {
            return inspectVirtualCamera;
        }

        public string GetMessage()
        {
            return _fuseItem == null ? inspectMessage : string.Empty;
        }

        public bool IsInspectable()
        {
            return isInspectable;
        }

        public bool IsExpectingItem(out ItemInfoSO itemType)
        {
            itemType = expectedItem;
            return _fuseItem == null;
        }

        public bool HasItem()
        {
            return _fuseItem != null;
        }

        public bool ShouldPlayInspectAnimation()
        {
            return true;
        }

        public bool TryItem(IItem item)
        {
            bool isExpectingItem = IsExpectingItem(out ItemInfoSO itemInfo);
            if (isExpectingItem && item.GetItemInfo() == itemInfo)
            {
                item.Consume();
                _fuseItem = item;
                fuseInPlaceMesh.gameObject.SetActive(true);

                // TODO: Play insertion audio.

                PowerChanged?.Invoke(true);

                return true;
            }

            return false;
        }

        public IItem TryTakeItem()
        {
            IItem item = _fuseItem;
            _fuseItem = null;
            fuseInPlaceMesh.gameObject.SetActive(false);

            PowerChanged?.Invoke(false);
            // TODO: Play removal audio.

            return item;
        }
    }
}