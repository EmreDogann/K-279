using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interactables;
using UnityEngine;

namespace Rooms
{
    public class RoomSwitcher : MonoBehaviour
    {
        [SerializeField] private float blackScreenWaitTime;
        [SerializeField] private CameraConfiner2DSwitcher cameraConfiner2DSwitcher;

        [SerializeField] private RoomType startingRoom;
        [SerializeField] private bool loadStartingRoomOnAwake;

        private List<Room> _rooms;
        private Room _currentRoom;

        private void Start()
        {
            _rooms = FindObjectsOfType<MonoBehaviour>(true)
                .OfType<Room>()
                .Where(a => a.isActiveAndEnabled)
                .ToList();

            _currentRoom = GetRoom(startingRoom);
            if (loadStartingRoomOnAwake)
            {
                _currentRoom.ActivateRoom();
                cameraConfiner2DSwitcher.SwitchConfinerTarget(_currentRoom.GetCameraBounds());
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
            if (_currentRoom)
            {
                yield return _currentRoom.DeactivateRoom(newRoom.GetRoomType());
                newRoom.PrepareRoom();
            }

            cameraConfiner2DSwitcher.SwitchConfinerTarget(newRoom.GetCameraBounds());
            yield return new WaitForSecondsRealtime(blackScreenWaitTime);

            if (_currentRoom)
            {
                newRoom.ActivateRoom(_currentRoom.GetRoomType());
            }
            else
            {
                newRoom.ActivateRoom();
            }

            roomSwitchedCallback?.Invoke();
            _currentRoom = newRoom;
        }
    }
}