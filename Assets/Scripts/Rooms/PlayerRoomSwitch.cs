using Lights;
using UnityEngine;

namespace Rooms
{
    public class PlayerRoomSwitch : RoomSwitchListener
    {
        [SerializeField] private RoomLight playerLight;

        public override void OnRoomActivate(RoomData roomData)
        {
            transform.position = roomData.StartingPosition.position;
            playerLight.TurnOnLight(roomData.LightFadeDuration);
        }

        public override void OnRoomDeactivate(RoomData roomData)
        {
            playerLight.TurnOffLight(roomData.LightFadeDuration);
        }
    }
}