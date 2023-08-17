using UnityEngine;

public abstract class RoomSwitchListener : MonoBehaviour
{
    public virtual void OnEnable()
    {
        Room.OnRoomActivate += OnRoomActivate;
    }

    public virtual void OnDisable()
    {
        Room.OnRoomActivate -= OnRoomActivate;
    }

    public abstract void OnRoomActivate(RoomData roomData);
}