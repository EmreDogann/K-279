using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Interactables;
using Lights;
using MyBox;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

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
        public SplineContainer RoomPath;
        public int PathIndex;
        public Transform StartingPosition;
        public float LightFadeDuration;
    }

    [Serializable]
    public class RoomAmbience
    {
        public AudioSO audio;
        public bool playInConnectingRooms;
        [ConditionalField(nameof(playInConnectingRooms))] public bool useOriginalAudioVolume = true;
        [ConditionalField(nameof(playInConnectingRooms))] public float connectingRoomVolume = 1.0f;
    }

    public class Room : MonoBehaviour
    {
        [SerializeField] private RoomType roomType;
        [SerializeField] private List<RoomAmbience> roomAmbiences;

        [Separator("Lights")]
        [SerializeField] private float lightFadeDuration = 1.0f;
        [field: SerializeReference] public bool LightsOn { get; private set; } = true;

        [Separator("Room Data")]
        [SerializeField] private Collider2D cameraBounds;
        [SerializeField] private List<Door> roomDoors;
        [SerializeField] private List<RoomLight> roomLights;
        [SerializeField] private SplineContainer roomPath;

        public static event Action<bool, float> OnLightsSwitch;
        public static event Action<RoomData> OnRoomPrepare;
        public static event Action<RoomData> OnRoomActivate;
        public static event Action<RoomData> OnRoomDeactivate;

        private BoxCollider _roomBounds;

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

            // Active then deactivate lights that start disabled so that their awake functions can get called.
            foreach (RoomLight roomLight in roomLights)
            {
                if (!roomLight.gameObject.activeSelf)
                {
                    roomLight.gameObject.SetActive(true);
                    roomLight.gameObject.SetActive(false);
                }
            }

            var roomBoundsCollider = GetComponentsInChildren<BoxCollider>();

            foreach (BoxCollider bounds in roomBoundsCollider)
            {
                if (bounds.gameObject.CompareTag("RoomBounds"))
                {
                    _roomBounds = bounds;
                    break;
                }
            }

            roomPath = GetComponentInChildren<SplineContainer>();

            if (_roomBounds == null)
            {
                Debug.LogError(
                    $"{name} Error: Room bounds not found! Please create a BoxCollider game object" +
                    "tagged \"RoomBounds\" that encapsulates the entire room.");
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

            roomPath = GetComponentInChildren<SplineContainer>();
        }

        public Vector3 GetDoorSpawnPoint(int doorIndex)
        {
            return roomDoors[doorIndex].GetSpawnPoint().position;
        }

        public RoomType GetRoomType()
        {
            return roomType;
        }

        public Collider2D GetCameraBounds()
        {
            return cameraBounds;
        }

        public bool ContainsPoint(Vector3 point)
        {
            return _roomBounds.bounds.Contains(point);
        }

        public void PrepareRoom(RoomType exitingRoom)
        {
            foreach (RoomLight roomLight in roomLights.Where(roomLight => roomLight.CanBeControlledByRoom()))
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
                    RoomPath = roomPath,
                    PathIndex = GetNearestSplineIndex(door.GetSpawnPoint().position),
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

        public void ActivateRoom(bool setPlayerPosition)
        {
            if (LightsOn)
            {
                foreach (RoomLight roomLight in roomLights.Where(roomLight => roomLight.CanBeControlledByRoom()))
                {
                    roomLight.TurnOnLight(lightFadeDuration);
                }

                OnLightsSwitch?.Invoke(true, lightFadeDuration);
            }

            // Uses first door
            if (roomDoors.Count > 0)
            {
                OnRoomActivate?.Invoke(new RoomData
                {
                    RoomPath = roomPath,
                    PathIndex = setPlayerPosition ? GetNearestSplineIndex(roomDoors[0].GetSpawnPoint().position) : 0,
                    StartingPosition = setPlayerPosition ? roomDoors[0].GetSpawnPoint() : null,
                    LightFadeDuration = lightFadeDuration
                });
            }

            foreach (RoomAmbience roomAmbience in roomAmbiences)
            {
                roomAmbience.audio.Play2D(true, 2.0f);
            }
        }

        public void ActivateRoom(RoomType exitingRoom, bool setPlayerPosition = true)
        {
            if (LightsOn)
            {
                foreach (RoomLight roomLight in roomLights.Where(roomLight => roomLight.CanBeControlledByRoom()))
                {
                    roomLight.TurnOnLight(lightFadeDuration);
                }

                OnLightsSwitch?.Invoke(true, lightFadeDuration);
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
                    RoomPath = roomPath,
                    PathIndex = setPlayerPosition ? GetNearestSplineIndex(door.GetSpawnPoint().position) : 0,
                    StartingPosition = setPlayerPosition ? door.GetSpawnPoint() : null,
                    LightFadeDuration = lightFadeDuration
                });
                break;
            }

            foreach (RoomAmbience roomAmbience in roomAmbiences)
            {
                roomAmbience.audio.Play2D(true, 2.0f);
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
                    RoomPath = roomPath,
                    PathIndex = GetNearestSplineIndex(door.GetSpawnPoint().position),
                    StartingPosition = door.GetSpawnPoint(),
                    LightFadeDuration = lightFadeDuration
                });
                break;
            }

            foreach (RoomAmbience roomAmbience in roomAmbiences)
            {
                roomAmbience.audio.Stop(AudioHandle.Invalid, true, 2.0f);
            }


            return StartCoroutine(WaitForLightsOff(lightFadeDuration));
        }

        public void ControlLightState(LightState state)
        {
            foreach (RoomLight roomLight in roomLights.Where(roomLight => roomLight.CanBeControlledByRoom()))
            {
                roomLight.ChangeLightState(state);
            }
        }

        public Coroutine ControlLights(bool turnOn, float duration)
        {
            LightsOn = turnOn;

            if (turnOn)
            {
                return StartCoroutine(WaitForLightsOn(duration));
            }

            return StartCoroutine(WaitForLightsOff(duration));
        }

        private IEnumerator WaitForLightsOff(float duration)
        {
            foreach (RoomLight roomLight in roomLights.Where(roomLight => roomLight.CanBeControlledByRoom()))
            {
                roomLight.TurnOffLight(duration);
                OnLightsSwitch?.Invoke(false, duration);
            }

            foreach (RoomLight roomLight in roomLights.Where(roomLight => roomLight.CanBeControlledByRoom()))
            {
                while (roomLight.IsOn())
                {
                    yield return null;
                }
            }
        }

        private IEnumerator WaitForLightsOn(float duration)
        {
            foreach (RoomLight roomLight in roomLights.Where(roomLight => roomLight.CanBeControlledByRoom()))
            {
                roomLight.TurnOnLight(duration);
                OnLightsSwitch?.Invoke(true, duration);
            }

            foreach (RoomLight roomLight in roomLights.Where(roomLight => roomLight.CanBeControlledByRoom()))
            {
                while (!roomLight.IsOn())
                {
                    yield return null;
                }
            }
        }

        private int GetNearestSplineIndex(Vector3 point)
        {
            float closestDistance = Mathf.Infinity;
            int closestIndex = 0;

            int index = 0;
            foreach (Spline path in roomPath.Splines)
            {
                float distance = SplineUtility.GetNearestPoint(path, roomPath.transform.InverseTransformPoint(point),
                    out float3 _, out float _);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = index;
                }

                index++;
            }

            return closestIndex;
        }

        
    }
}