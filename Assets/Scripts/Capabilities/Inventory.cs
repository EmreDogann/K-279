using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Capabilities
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<IItem> _items;

        private void Awake()
        {
            _items = new List<IItem>();
        }

        private void OnDestroy()
        {
            foreach (IItem item in _items)
            {
                item.OnConsumed -= ItemConsumed;
            }
        }

        public void AddItem(IItem item)
        {
            _items.Add(item);
            item.OnConsumed += ItemConsumed;
        }

        public bool ContainsItem(IItem item)
        {
            return _items.Contains(item);
        }

        public bool ContainsItemType(ItemType itemType)
        {
            foreach (IItem item in _items)
            {
                if (item.GetItemType() == itemType)
                {
                    return true;
                }
            }

            return false;
        }

        private void ItemConsumed(IItem item)
        {
            _items.Remove(item);
            item.OnConsumed -= ItemConsumed;
        }
    }
}