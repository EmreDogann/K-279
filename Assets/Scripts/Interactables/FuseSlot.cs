using System;
using Cinemachine;
using Inspect.Views.Triggers;
using Items;
using MyBox;
using ScriptableObjects;
using UnityEngine;

namespace Interactables
{
    public class FuseSlot : MonoBehaviour, IItemUser
    {
        [SerializeField] private Transform fuseInPlaceMesh;

        [Separator("Inspection")]
        [SerializeField] private ItemInfoSO expectedItem;
        [SerializeField] private ViewTrigger missingFuseViewTrigger;

        [SerializeField] private CinemachineVirtualCamera inspectVirtualCamera;

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

        public CinemachineVirtualCamera GetCameraAngle()
        {
            return inspectVirtualCamera;
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

        public bool TryItem(IItem item)
        {
            if (item == null)
            {
                missingFuseViewTrigger.TriggerView();
                return false;
            }

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