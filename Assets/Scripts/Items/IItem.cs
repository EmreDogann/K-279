using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public delegate void ConsumeHandler(IItem item);

    public interface IItem
    {
        event ConsumeHandler OnConsumed;

        public ItemInfoSO GetItemInfo();
        public float GetResourceQuantity();
        public Transform GetTransform();
        public void Pickup();
        public void Consume();
    }
}