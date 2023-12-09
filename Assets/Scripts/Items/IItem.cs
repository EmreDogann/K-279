using ScriptableObjects;

namespace Items
{
    public delegate void ItemHandler(IItem item);

    public interface IItem
    {
        event ItemHandler OnConsumed;

        public ItemInfoSO GetItemInfo();
        public void Pickup();
        public void Consume();
    }
}