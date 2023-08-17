public class PlayerRoomSwitch : RoomSwitchListener
{
    public override void OnRoomActivate(RoomData roomData)
    {
        transform.position = roomData.StartingPosition.position;
    }
}