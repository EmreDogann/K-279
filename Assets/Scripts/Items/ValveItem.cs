using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public class ValveItem : MonoBehaviour, IItem
    {
        [SerializeField] private ItemInfoSO itemInfo;

        private bool _isAvailable;

        private void Start()
        {
            ItemManager.Instance.RegisterItem(this);
        }

        public ItemType GetItemType()
        {
            return ItemType.Valve;
        }

        public ItemInfoSO GetItemInfo()
        {
            return itemInfo;
        }

        public void Pickup()
        {
            _isAvailable = false;
        }

        public void Consume()
        {
            _isAvailable = true;
        }

        public bool IsAvailable()
        {
            return _isAvailable;
        }
    }
}