using UnityEngine;

namespace Rooms
{
    public abstract class RoomSwitchListener : MonoBehaviour
    {
        public virtual void Awake()
        {
            Room.OnRoomPrepare += OnRoomPrepare;
            Room.OnRoomActivate += OnRoomActivate;
            Room.OnRoomDeactivate += OnRoomDeactivate;
        }

        public virtual void OnDestroy()
        {
            Room.OnRoomPrepare -= OnRoomPrepare;
            Room.OnRoomActivate -= OnRoomActivate;
            Room.OnRoomDeactivate -= OnRoomDeactivate;
        }

        public abstract void OnRoomPrepare(RoomData roomData);
        public abstract void OnRoomActivate(RoomData roomData);
        public abstract void OnRoomDeactivate(RoomData roomData);
    }
}