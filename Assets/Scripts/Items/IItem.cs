using ScriptableObjects;

namespace Items
{
    public enum ItemType
    {
        Ammo,
        Flare,
        Fuse,
        Valve
    }

    public delegate void ConsumeHandler(IItem item);

    public interface IItem
    {
        event ConsumeHandler OnConsumed;

        public ItemType GetItemType();
        public ItemInfoSO GetItemInfo();
        public float GetResourceQuantity();
        public void Reset();
        public void Pickup();
        public void Consume();
        public bool IsAvailable();
    }
}