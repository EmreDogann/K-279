using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Interactables;
using Lights;
using MyBox;
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

    [Serializable]
    public class RoomAmbience
    {
        public AudioSO audio;
        public bool playInConnectingRooms;
        [ConditionalField(nameof(playInConnectingRooms))] public bool useOriginalAudioVolume = true;
        [ConditionalField(nameof(useOriginalAudioVolume), true)] public float connectingRoomVolume = 1.0f;
    }

    public class Room : MonoBehaviour
    {
        [SerializeField] private RoomType roomType;
        [SerializeField] private List<RoomAmbience> roomAmbiences;

        [Separator("Lights")]
        [SerializeField] private float lightFadeDuration = 1.0f;
        [field: SerializeReference] public bool ActivateLightsOnRoomLoad { get; private set; } = true;

        [Separator("Room Data")]
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

        public List<RoomAmbience> GetRoomAmbiences()
        {
            return roomAmbiences;
        }

        public List<Door> GetDoors()
        {
            return roomDoors;
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

            foreach (RoomAmbience roomAmbience in roomAmbiences)
            {
                roomAmbience.audio.Play2D(false, 2.0f);
            }
        }

        public void ActivateRoom(RoomType exitingRoom)
        {
            if (ActivateLightsOnRoomLoad)
            {
                foreach (RoomLight roomLight in roomLights)
                {
                    roomLight.TurnOnLight(lightFadeDuration);
                }
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

            foreach (RoomAmbience roomAmbience in roomAmbiences)
            {
                roomAmbience.audio.Play2D(false, 2.0f);
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

            foreach (RoomAmbience roomAmbience in roomAmbiences)
            {
                roomAmbience.audio.Stop(AudioHandle.Invalid, false, 2.0f);
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