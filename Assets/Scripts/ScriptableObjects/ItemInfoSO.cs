using Items;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Item Info", menuName = "Items/Item Info", order = 0)]
    public class ItemInfoSO : ScriptableObject
    {
        public ItemType itemType;
        public Sprite inspectImage;
        public string itemName;
        public float resourceQuantity;
    }
}