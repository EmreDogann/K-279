using ScriptableObjects;

namespace Items
{
    public enum ItemType
    {
        Valve,
        Ammo,
        Flare,
        Fuse
    }

    public interface IItem
    {
        public ItemType GetItemType();
        public ItemInfoSO GetItemInfo();
        public void Pickup();
        public void Consume();
        public bool IsAvailable();
    }
}