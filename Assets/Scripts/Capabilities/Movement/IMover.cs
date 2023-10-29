namespace Capabilities.Movement
{
    public delegate void SwitchingDirection(bool isFacingRight);

    public interface IMover
    {
        event SwitchingDirection OnSwitchingDirection;

        public void StartMovement();
        public void StopMovement();
        public void SwitchDirection();
    }
}