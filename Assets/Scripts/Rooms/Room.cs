using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interactables;
using Lights;
using UnityEngine;

namespace Rooms
{
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
        public float LightFadeDuration;
    }

    public class Room : MonoBehaviour
    {
        [SerializeField] private RoomType roomType;
        [SerializeField] private float lightFadeDuration = 1.0f;

        [SerializeField] private Collider2D cameraBounds;
        [SerializeField] private List<Door> roomDoors;
        [SerializeField] private List<RoomLight> roomLights;

        public static event Action<RoomData> OnRoomPrepare;
        public static event Action<RoomData> OnRoomActivate;
        public static event Action<RoomData> OnRoomDeactivate;

        private void Awake()
        {
            if (roomDoors == null)
            {
                roomDoors = GetComponentsInChildren<Door>().ToList();
            }

            if (roomLights == null)
            {
                roomLights = GetComponentsInChildren<RoomLight>().ToList();
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

            var lightsFound = GetComponentsInChildren<RoomLight>();
            if (lightsFound.Length != roomLights.Count)
            {
                foreach (RoomLight roomLight in lightsFound)
                {
                    if (!roomLights.Contains(roomLight))
                    {
                        roomLights.Add(roomLight);
                    }
                }
            }
        }

        public RoomType GetRoomType()
        {
            return roomType;
        }

        public Collider2D GetCameraBounds()
        {
            return cameraBounds;
        }

        public void PrepareRoom(RoomType exitingRoom)
        {
            foreach (RoomLight roomLight in roomLights)
            {
                roomLight.TurnOffLight();
            }

            foreach (Door door in roomDoors)
            {
                if (door.GetConnectingRoom() != exitingRoom)
                {
                    continue;
                }

                OnRoomPrepare?.Invoke(new RoomData
                {
                    StartingPosition = door.GetSpawnPoint(),
                    LightFadeDuration = lightFadeDuration
                });
                break;
            }
        }

        public void ActivateRoom()
        {
            // Uses first room
            foreach (Door door in roomDoors)
            {
                OnRoomActivate?.Invoke(new RoomData
                {
                    StartingPosition = door.GetSpawnPoint(),
                    LightFadeDuration = lightFadeDuration
                });
                break;
            }
        }

        public void ActivateRoom(RoomType exitingRoom)
        {
            foreach (RoomLight roomLight in roomLights)
            {
                roomLight.TurnOnLight(lightFadeDuration);
            }

            foreach (Door door in roomDoors)
            {
                if (door.GetConnectingRoom() != exitingRoom)
                {
                    continue;
                }

                door.PlayClosingAudio();

                OnRoomActivate?.Invoke(new RoomData
                {
                    StartingPosition = door.GetSpawnPoint(),
                    LightFadeDuration = lightFadeDuration
                });
                break;
            }
        }

        public Coroutine DeactivateRoom(RoomType exitingRoom)
        {
            foreach (Door door in roomDoors)
            {
                if (door.GetConnectingRoom() != exitingRoom)
                {
                    continue;
                }

                OnRoomDeactivate?.Invoke(new RoomData
                {
                    StartingPosition = door.GetSpawnPoint(),
                    LightFadeDuration = lightFadeDuration
                });
                break;
            }

            return StartCoroutine(WaitForLightsOff(lightFadeDuration));
        }

        public Coroutine ControlLights(bool turnOn, float duration)
        {
            if (turnOn)
            {
                return StartCoroutine(WaitForLightsOn(duration));
            }

            return StartCoroutine(WaitForLightsOff(duration));
        }

        private IEnumerator WaitForLightsOff(float duration)
        {
            foreach (RoomLight roomLight in roomLights)
            {
                roomLight.TurnOffLight(duration);
            }

            foreach (RoomLight roomLight in roomLights)
            {
                while (roomLight.IsOn())
                {
                    yield return null;
                }
            }
        }

        private IEnumerator WaitForLightsOn(float duration)
        {
            foreach (RoomLight roomLight in roomLights)
            {
                roomLight.TurnOnLight(duration);
            }

            foreach (RoomLight roomLight in roomLights)
            {
                while (!roomLight.IsOn())
                {
                    yield return null;
                }
            }
        }
    }
}