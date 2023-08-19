using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interactables;
using Lights;
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
                PlayDoorAmbiances(_currentRoom.GetDoors());
                cameraConfiner2DSwitcher.SwitchConfinerTarget(_currentRoom.GetCameraBounds());
            }
        }

        private void OnEnable()
        {
            Door.OnRoomSwitching += SwitchRoom;
            LightControl.OnLightControl += OnLightControl;
        }

        private void OnDisable()
        {
            Door.OnRoomSwitching -= SwitchRoom;
            LightControl.OnLightControl -= OnLightControl;
        }

        private void OnLightControl(bool turnOn, float duration)
        {
            StartCoroutine(WaitForLights(turnOn, duration));
        }

        private Room GetRoom(RoomType roomType)
        {
            return _rooms.Find(x => x.GetRoomType() == roomType);
        }

        private IEnumerator WaitForLights(bool turnOn, float duration)
        {
            if (_currentRoom)
            {
                yield return _currentRoom.ControlLights(turnOn, duration);
            }
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
                StopDoorAmbiances(_currentRoom.GetDoors());
                newRoom.PrepareRoom(_currentRoom.GetRoomType());
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

            PlayDoorAmbiances(newRoom.GetDoors());

            roomSwitchedCallback?.Invoke();
            _currentRoom = newRoom;
        }

        private void PlayDoorAmbiances(List<Door> doors)
        {
            foreach (Door door in doors)
            {
                if (!door.PlayConnectingRoomAmbience)
                {
                    continue;
                }

                var roomAmbiences = GetRoom(door.GetConnectingRoom()).GetRoomAmbiences();

                foreach (RoomAmbience roomAmbience in roomAmbiences)
                {
                    if (roomAmbience.playInConnectingRooms)
                    {
                        if (roomAmbience.useOriginalAudioVolume)
                        {
                            roomAmbience.audio.Play(door.transform.position, true, 0.5f);
                        }
                        else
                        {
                            roomAmbience.audio.Play(door.transform.position, true, 0.5f,
                                roomAmbience.connectingRoomVolume);
                        }
                    }
                }
            }
        }

        private void StopDoorAmbiances(List<Door> doors)
        {
            foreach (Door door in doors)
            {
                if (!door.PlayConnectingRoomAmbience)
                {
                    continue;
                }

                var roomAmbiences = GetRoom(door.GetConnectingRoom()).GetRoomAmbiences();

                foreach (RoomAmbience roomAmbience in roomAmbiences)
                {
                    if (roomAmbience.playInConnectingRooms)
                    {
                        roomAmbience.audio.StopAll();
                    }
                }
            }
        }
    }
}