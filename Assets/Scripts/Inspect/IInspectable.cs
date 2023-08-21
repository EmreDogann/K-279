using Cinemachine;
using Items;

namespace Inspect
{
    public interface IInspectable
    {
        public CinemachineVirtualCamera GetCameraAngle();
        public string GetMessage();

        public bool IsInspectable();
        public bool IsExpectingItem(out ItemType type);

        public bool HasAvailableItem();
        public bool TryItem(IItem item);
        public IItem TryTakeItem();
    }
}