using Cinemachine;

namespace Inspect
{
    public interface IInspectable
    {
        public CinemachineVirtualCamera GetCameraAngle();
        public string GetMessage();

        public bool IsInspectable();
    }
}