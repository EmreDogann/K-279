using Capabilities.Movement;

namespace Rooms
{
    public class PlayerRoomSwitch : RoomSwitchListener
    {
        public override void OnRoomPrepare(RoomData roomData) {}

        public override void OnRoomActivate(RoomData roomData)
        {
            GetComponent<IMover>()?.SetMovementParams(roomData);
        }

        public override void OnRoomDeactivate(RoomData roomData) {}
    }
}