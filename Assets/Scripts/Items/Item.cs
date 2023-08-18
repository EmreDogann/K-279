using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] private ItemInfoSO itemInfo;

        private bool _isAvailable;

        public event ConsumeHandler OnConsumed;
        event ConsumeHandler IItem.OnConsumed
        {
            add => OnConsumed += value;
            remove => OnConsumed -= value;
        }

        private void Start()
        {
            ItemManager.Instance.RegisterItem(this);
        }

        public ItemType GetItemType()
        {
            return itemInfo.itemType;
        }

        public ItemInfoSO GetItemInfo()
        {
            return itemInfo;
        }

        public float GetResourceQuantity()
        {
            return itemInfo.resourceQuantity;
        }

        public void Reset()
        {
            gameObject.SetActive(true);
        }

        public void Pickup()
        {
            _isAvailable = false;
            gameObject.SetActive(false);
        }

        public void Consume()
        {
            _isAvailable = true;
            OnConsumed?.Invoke(this);
        }

        public bool IsAvailable()
        {
            return _isAvailable;
        }
    }
}