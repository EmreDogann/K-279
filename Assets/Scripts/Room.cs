using System;
using System.Collections.Generic;
using System.Linq;
using Interactables;
using UnityEngine;

public enum RoomType
{
    CaptainQuarters,
    CrewQuarters,
    Engine,
    Hallway,
    Navigation,
    Torpedo
}

public class RoomData
{
    public Transform StartingPosition;
    public Collider2D CameraBounds;
}

public class Room : MonoBehaviour
{
    [SerializeField] private RoomType roomType;
    [SerializeField] private Collider2D cameraBounds;
    [SerializeField] private List<Door> roomDoors;

    public static event Action<RoomData> OnRoomActivate;

    private void Awake()
    {
        if (roomDoors == null)
        {
            roomDoors = GetComponentsInChildren<Door>().ToList();
        }
    }

    private void OnValidate()
    {
        var doorsFound = GetComponentsInChildren<Door>();
        if (doorsFound.Length != roomDoors.Count)
        {
            foreach (Door door in doorsFound)
            {
                if (!roomDoors.Contains(door))
                {
                    roomDoors.Add(door);
                }
            }
        }
    }

    public RoomType GetRoomType()
    {
        return roomType;
    }

    public void ActivateRoom()
    {
        // Uses first room
        foreach (Door door in roomDoors)
        {
            OnRoomActivate?.Invoke(new RoomData
            {
                StartingPosition = door.GetSpawnPoint(),
                CameraBounds = cameraBounds
            });
            break;
        }
    }

    public void ActivateRoom(RoomType exitingRoom)
    {
        foreach (Door door in roomDoors)
        {
            if (door.GetConnectingRoom() == exitingRoom)
            {
                door.PlayClosingAudio();

                OnRoomActivate?.Invoke(new RoomData
                {
                    StartingPosition = door.GetSpawnPoint(),
                    CameraBounds = cameraBounds
                });
                break;
            }
        }
    }

    public void DeactivateRoom() {}
}