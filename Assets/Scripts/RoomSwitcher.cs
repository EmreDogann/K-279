using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interactables;
using MyBox;
using UI;
using UnityEngine;

public class RoomSwitcher : MonoBehaviour
{
    [SerializeField] private CanvasGroupFade canvasFader;
    [SerializeField] private float blackScreenWaitTime;

    [SerializeField] private bool LoadStartingRoomOnAwake;

    [ConditionalField(nameof(LoadStartingRoomOnAwake))] [SerializeField] private RoomType startingRoom;

    private List<Room> _rooms;
    private Room currentRoom;

    private void Start()
    {
        _rooms = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<Room>()
            .Where(a => a.isActiveAndEnabled)
            .ToList();

        if (LoadStartingRoomOnAwake)
        {
            currentRoom = GetRoom(startingRoom);
            currentRoom.ActivateRoom();
        }
    }

    private void OnEnable()
    {
        Door.OnRoomSwitching += SwitchRoom;
    }

    private void OnDisable()
    {
        Door.OnRoomSwitching -= SwitchRoom;
    }

    private Room GetRoom(RoomType roomType)
    {
        return _rooms.Find(x => x.GetRoomType() == roomType);
    }

    private void SwitchRoom(RoomType roomType, Action roomSwitchedCallback)
    {
        foreach (Room room in _rooms)
        {
            if (room.GetRoomType() == roomType)
            {
                StartCoroutine(TransitionRooms(room, roomSwitchedCallback));
                break;
            }
        }
    }

    private IEnumerator TransitionRooms(Room newRoom, Action roomSwitchedCallback)
    {
        if (currentRoom)
        {
            currentRoom.DeactivateRoom();
        }

        yield return canvasFader.ToggleFade();
        yield return new WaitForSecondsRealtime(blackScreenWaitTime);

        if (currentRoom)
        {
            newRoom.ActivateRoom(currentRoom.GetRoomType());
        }
        else
        {
            newRoom.ActivateRoom();
        }

        yield return canvasFader.ToggleFade(0.5f);

        roomSwitchedCallback?.Invoke();

        currentRoom = newRoom;
    }
}