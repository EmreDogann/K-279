using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] private ItemInfoSO itemInfo;

        private bool _isAvailable;

        public event ItemHandler OnConsumed;
        event ItemHandler IItem.OnConsumed
        {
            add => OnConsumed += value;
            remove => OnConsumed -= value;
        }

        private void Start()
        {
            ItemManager.Instance.RegisterItem(this);
        }

        public ItemInfoSO GetItemInfo()
        {
            return itemInfo;
        }

        public void Pickup()
        {
            gameObject.SetActive(false);
        }

        public void Consume()
        {
            OnConsumed?.Invoke(this);
        }
    }
}