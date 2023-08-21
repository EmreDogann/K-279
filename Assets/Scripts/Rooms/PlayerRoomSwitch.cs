namespace Rooms
{
    public class PlayerRoomSwitch : RoomSwitchListener
    {
        public override void OnRoomPrepare(RoomData roomData) {}

        public override void OnRoomActivate(RoomData roomData)
        {
            if (roomData.StartingPosition != null)
            {
                transform.position = roomData.StartingPosition.position;
            }
        }

        public override void OnRoomDeactivate(RoomData roomData) {}
    }
}