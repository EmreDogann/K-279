using Cinemachine;
using ScriptableObjects;

namespace Items
{
    public interface IItemUser
    {
        public CinemachineVirtualCamera GetCameraAngle();
        public bool IsExpectingItem(out ItemInfoSO expectedItem);
        public bool HasItem();
        public bool TryItem(IItem item);
        public IItem TryTakeItem();
    }
}