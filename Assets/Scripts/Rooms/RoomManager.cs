using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interactables;
using Lights;
using MyBox;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Rooms
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private float roomLoadWaitTime;
        [SerializeField] private CameraConfiner2DSwitcher cameraConfiner2DSwitcher;

        [Separator("Room Settings")]
        [SerializeField] private bool loadStartingRoomOnAwake;
        [SerializeField] private RoomType startingRoom;
        [SerializeField] private RoomType startingRoomEnteringFrom;

        [SerializeField] private List<Room> rooms;
        [ReadOnly] [SerializeField] private Room currentRoom;

        private bool _switchingInProgress;

        private void Awake()
        {
            rooms = GetComponentsInChildren<Room>(true).ToList();
        }

        private void Start()
        {
            if (currentRoom != null || _switchingInProgress)
            {
                return;
            }

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
                currentRoom.ActivateRoom(startingRoomEnteringFrom, loadStartingRoomOnAwake);
                PlayDoorAmbiances(currentRoom.GetDoors());
                cameraConfiner2DSwitcher.SwitchConfinerTarget(currentRoom.GetCameraBounds());
            }
        }

        private void OnValidate()
        {
            rooms = GetComponentsInChildren<Room>(true).ToList();
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
            return rooms.Find(x => x.GetRoomType() == roomType);
        }

        public Room GetRoomAtPoint(Vector3 point)
        {
            return rooms.Find(x => x.ContainsPoint(point));
        }

        private IEnumerator WaitForLights(bool turnOn, float duration)
        {
            if (currentRoom)
            {
                yield return currentRoom.ControlLights(turnOn, duration);
            }
        }

        public void SwitchRoom(RoomType roomType, float transitionWaitTime = -1.0f, Action roomSwitchedCallback = null)
        {
            foreach (Room room in rooms)
            {
                if (room.GetRoomType() == roomType)
                {
                    StartCoroutine(TransitionRooms(room, transitionWaitTime, roomSwitchedCallback));
                    break;
                }
            }
        }

        private IEnumerator TransitionRooms(Room newRoom, float roomLoadWaitTimeOverride, Action roomSwitchedCallback)
        {
            _switchingInProgress = true;

            if (currentRoom)
            {
                yield return currentRoom.DeactivateRoom(newRoom.GetRoomType());
                StopDoorAmbiances(currentRoom.GetDoors());
                newRoom.PrepareRoom(currentRoom.GetRoomType());
            }

            cameraConfiner2DSwitcher.SwitchConfinerTarget(newRoom.GetCameraBounds());
            yield return new WaitForSecondsRealtime(roomLoadWaitTimeOverride > 0.0f
                ? roomLoadWaitTimeOverride
                : roomLoadWaitTime);

            if (currentRoom)
            {
                newRoom.ActivateRoom(currentRoom.GetRoomType());
            }

            PlayDoorAmbiances(newRoom.GetDoors());

            roomSwitchedCallback?.Invoke();
            currentRoom = newRoom;

            _switchingInProgress = false;
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
            foreach (Room room in rooms)
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

#if UNITY_EDITOR
        [CustomEditor(typeof(RoomManager))]
        public class RoomManagerEditor : Editor
        {
            private SerializedProperty _roomLoadWaitTimeProp;
            private SerializedProperty _cameraConfiner2DSwitcherProp;

            private SerializedProperty _startingRoomProp;
            private SerializedProperty _startingRoomEnteringFromProp;
            private SerializedProperty _loadStartingRoomOnAwakeProp;

            private SerializedProperty _roomsProp;
            private SerializedProperty _currentRoomProp;

            private int _selectedIndex;
            private string[] _availableOptions = {};
            private string[] _availableOptionsNicified = {};

            private void OnEnable()
            {
                _roomLoadWaitTimeProp = serializedObject.FindProperty(nameof(roomLoadWaitTime));
                _cameraConfiner2DSwitcherProp = serializedObject.FindProperty(nameof(cameraConfiner2DSwitcher));

                _startingRoomProp = serializedObject.FindProperty(nameof(startingRoom));
                _startingRoomEnteringFromProp = serializedObject.FindProperty(nameof(startingRoomEnteringFrom));
                _loadStartingRoomOnAwakeProp = serializedObject.FindProperty(nameof(loadStartingRoomOnAwake));

                _roomsProp = serializedObject.FindProperty(nameof(rooms));
                _currentRoomProp = serializedObject.FindProperty(nameof(currentRoom));

                (_availableOptions, _availableOptionsNicified) =
                    GetAvailableRooms((RoomType)_startingRoomProp.enumValueIndex);
                _selectedIndex = EditorPrefs.GetInt("RoomManager_StartingRoomSelectedDoor");
            }

            private void OnDisable()
            {
                EditorPrefs.SetInt("RoomManager_StartingRoomSelectedDoor", _selectedIndex);
            }

            public override void OnInspectorGUI()
            {
                serializedObject.Update();

                EditorGUILayout.PropertyField(_roomLoadWaitTimeProp);
                EditorGUILayout.PropertyField(_cameraConfiner2DSwitcherProp);

                EditorGUILayout.PropertyField(_loadStartingRoomOnAwakeProp);
                if (_loadStartingRoomOnAwakeProp.boolValue)
                {
                    EditorGUI.BeginChangeCheck();

                    EditorGUILayout.PropertyField(_startingRoomProp);

                    if (EditorGUI.EndChangeCheck())
                    {
                        (_availableOptions, _availableOptionsNicified) =
                            GetAvailableRooms((RoomType)_startingRoomProp.enumValueIndex);

                        Enum.TryParse(_availableOptions[0], out RoomType startingDoor);
                        _startingRoomEnteringFromProp.enumValueIndex = (int)startingDoor;
                        _selectedIndex = 0;
                    }

                    if (_availableOptions.Length <= 0)
                    {
                        GUIStyle labelStyle = new GUIStyle();
                        labelStyle.alignment = TextAnchor.MiddleCenter;
                        labelStyle.normal.textColor = Color.red;
                        EditorGUILayout.LabelField("Room does not have any doors! Cannot spawn here!", labelStyle);
                    }
                    else
                    {
                        EditorGUI.BeginChangeCheck();
                        _selectedIndex = EditorGUILayout.Popup(_startingRoomEnteringFromProp.displayName,
                            _selectedIndex, _availableOptionsNicified);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Enum.TryParse(_availableOptions[_selectedIndex], out RoomType startingDoor);
                            _startingRoomEnteringFromProp.enumValueIndex = (int)startingDoor;
                        }
                    }
                }

                EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

                EditorGUILayout.PropertyField(_roomsProp);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(_currentRoomProp);
                EditorGUI.BeginDisabledGroup(false);

                EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
                foreach (RoomType roomType in Enum.GetValues(typeof(RoomType)))
                {
                    if (GUILayout.Button("Player  ->  " + ObjectNames.NicifyVariableName(roomType.ToString())))
                    {
                        ((RoomManager)serializedObject.targetObject).PlayerToRoom(roomType);
                    }
                }

                serializedObject.ApplyModifiedProperties();
            }

            private Tuple<string[], string[]> GetAvailableRooms(RoomType selectedRoom)
            {
                RoomManager roomManager = (RoomManager)serializedObject.targetObject;

                var availableRooms = new List<string>();
                var availableRoomsNicified = new List<string>();
                Room currentRoom = roomManager.GetRoom(selectedRoom);

                foreach (Door door in currentRoom.GetDoors())
                {
                    availableRooms.Add(door.GetConnectingRoom().ToString());
                    availableRoomsNicified.Add(ObjectNames.NicifyVariableName(door.GetConnectingRoom().ToString()));
                }

                return new Tuple<string[], string[]>(availableRooms.ToArray(), availableRoomsNicified.ToArray());
            }
        }
#endif
    }
}