using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interactables;
using Lights;
using MyBox;
using UnityEngine;

namespace Rooms
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private float roomLoadWaitTime;
        [SerializeField] private CameraConfiner2DSwitcher cameraConfiner2DSwitcher;

        [SerializeField] private RoomType startingRoom;
        [SerializeField] private bool loadStartingRoomOnAwake;
        [ConditionalField(nameof(loadStartingRoomOnAwake), true)]
        [SerializeField] private List<Room> _rooms;
        [ReadOnly] [SerializeField] private Room currentRoom;

        private void Awake()
        {
            _rooms = GetComponentsInChildren<Room>(true).ToList();
        }

        private void Start()
        {
            GameObject player = GameObject.FindWithTag("Player");
            currentRoom = loadStartingRoomOnAwake
                ? GetRoom(startingRoom)
                : GetRoomAtPoint(player.transform.position);

            if (currentRoom == null)
            {
                Debug.LogError("Room Manager Error: Initial room not found. Maybe player is out of bounds?");
            }
            else
            {
                currentRoom.ActivateRoom(loadStartingRoomOnAwake);
                PlayDoorAmbiances(currentRoom.GetDoors());
                cameraConfiner2DSwitcher.SwitchConfinerTarget(currentRoom.GetCameraBounds());
            }
        }

        private void OnValidate()
        {
            _rooms = GetComponentsInChildren<Room>(true).ToList();
        }

        private void OnEnable()
        {
            Door.OnRoomSwitching += SwitchRoom;
            LightManager.OnLightControl += OnLightControl;
        }

        private void OnDisable()
        {
            Door.OnRoomSwitching -= SwitchRoom;
            LightManager.OnLightControl -= OnLightControl;
        }

        private void OnLightControl(bool turnOn, float duration)
        {
            StartCoroutine(WaitForLights(turnOn, duration));
        }

        public Room GetRoom(RoomType roomType)
        {
            return _rooms.Find(x => x.GetRoomType() == roomType);
        }

        public Room GetRoomAtPoint(Vector3 point)
        {
            return _rooms.Find(x => x.ContainsPoint(point));
        }

        private IEnumerator WaitForLights(bool turnOn, float duration)
        {
            if (currentRoom)
            {
                yield return currentRoom.ControlLights(turnOn, duration);
            }
        }

        public void SwitchRoom(RoomType roomType, Action roomSwitchedCallback = null)
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
                yield return currentRoom.DeactivateRoom(newRoom.GetRoomType());
                StopDoorAmbiances(currentRoom.GetDoors());
                newRoom.PrepareRoom(currentRoom.GetRoomType());
            }

            cameraConfiner2DSwitcher.SwitchConfinerTarget(newRoom.GetCameraBounds());
            yield return new WaitForSecondsRealtime(roomLoadWaitTime);

            if (currentRoom)
            {
                newRoom.ActivateRoom(currentRoom.GetRoomType());
            }
            else
            {
                newRoom.ActivateRoom(true);
            }

            PlayDoorAmbiances(newRoom.GetDoors());

            roomSwitchedCallback?.Invoke();
            currentRoom = newRoom;
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

        private void PlayerToRoom(RoomType roomType)
        {
            GameObject player = GameObject.FindWithTag("Player");
            foreach (Room room in _rooms)
            {
                if (room.GetRoomType() == roomType)
                {
                    // Set player to spawn point of first door in the room's list.
                    player.transform.position = room.GetDoorSpawnPoint(0);
                    cameraConfiner2DSwitcher.SwitchConfinerTarget(room.GetCameraBounds());
                    break;
                }
            }
        }

        [ButtonMethod]
        private void PlayerToNavigation()
        {
            PlayerToRoom(RoomType.Navigation);
        }

        [ButtonMethod]
        private void PlayerToHallway()
        {
            PlayerToRoom(RoomType.Hallway);
        }

        [ButtonMethod]
        private void PlayerToCaptainsQuarters()
        {
            PlayerToRoom(RoomType.CaptainQuarters);
        }

        [ButtonMethod]
        private void PlayerToCrewQuarters()
        {
            PlayerToRoom(RoomType.CrewQuarters);
        }

        [ButtonMethod]
        private void PlayerToTorpedo()
        {
            PlayerToRoom(RoomType.Torpedo);
        }

        [ButtonMethod]
        private void PlayerToEngine()
        {
            PlayerToRoom(RoomType.Engine);
        }
    }
}