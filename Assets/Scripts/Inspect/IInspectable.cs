using Cinemachine;
using Items;

namespace Inspect
{
    public interface IInspectable
    {
        public CinemachineVirtualCamera GetCameraAngle();
        public string GetMessage();
        public bool IsInspectable();
        public bool IsExpectingItem();
        public ItemType GetExpectedItem();
        public bool TryItem(IItem item);
    }
}