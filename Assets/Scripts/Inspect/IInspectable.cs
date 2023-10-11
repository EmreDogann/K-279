using Cinemachine;
using Items;
using ScriptableObjects;

namespace Inspect
{
    public interface IInspectable
    {
        public CinemachineVirtualCamera GetCameraAngle();
        public string GetMessage();

        public bool IsInspectable();
        public bool IsExpectingItem(out ItemInfoSO item);
        public bool ShouldPlayInspectAnimation();
        public bool HasAvailableItem();
        public bool TryItem(IItem item);
        public IItem TryTakeItem();
    }
}