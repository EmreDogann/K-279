using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Interactables;
using Lights;
using MyBox;
using SceneLoading;
using ScriptableObjects.Rooms;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Rooms
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private float roomLoadWaitTime;

        [Separator("Room Settings")]
        [SerializeField] private bool loadStartingRoomOnAwake;
        [SerializeField] private RoomType startingRoom;
        [SerializeField] private RoomType startingRoomEnteringFrom;

        [SerializeField] private bool roomLogging;

        [SerializeField] private List<Room> rooms;
        [ReadOnly] [SerializeField] private Room currentRoom;

        private SerializedDictionary<RoomType, RoomConfig> _roomTypeToRoomConfig;
        private bool _switchingInProgress;

        private void Awake()
        {
            Room.OnRoomCreate += OnRoomLoaded;
            Room.OnRoomDestroy += OnRoomDestroy;
        }

        private void OnRoomLoaded(Room loadedRoom)
        {
            bool containsRoom = false;
            foreach (Room room in rooms)
            {
                if (room.RoomType() == loadedRoom.RoomType())
                {
                    containsRoom = true;
                    break;
                }
            }

            if (!containsRoom)
            {
                rooms.Add(loadedRoom);
            }
        }

        private void OnRoomDestroy(Room destroyRoom)
        {
            int index = 0;
            foreach (Room room in rooms)
            {
                if (room.RoomType() == destroyRoom.RoomType())
                {
                    break;
                }

                index++;
            }

            rooms.RemoveAt(index);
        }

        private void OnDestroy()
        {
            Room.OnRoomCreate -= OnRoomLoaded;
            Room.OnRoomDestroy -= OnRoomDestroy;
        }

        private void Start()
        {
            if (currentRoom != null || _switchingInProgress)
            {
                return;
            }

            GameObject player = GameState.Instance.GetPlayer;
            if (player == null)
            {
                Debug.LogError("ERROR: PlayerToRoom(): Player reference not found in GameState!");
                return;
            }

            currentRoom = loadStartingRoomOnAwake
                ? GetRoom(startingRoom)
                : GetRoomAtPoint(player.transform.position);

            if (currentRoom == null)
            {
                Debug.LogError("Room Manager Error: Initial room not found. Maybe player is out of bounds?");
            }
            else
            {
                currentRoom.SetRoomLogging(roomLogging);
                currentRoom.ActivateRoom(startingRoomEnteringFrom, loadStartingRoomOnAwake);
                PlayDoorAmbiances(currentRoom.Doors());
            }
        }

        private void OnEnable()
        {
            Door.OnRoomSwitching += SwitchRoom;
            LightManager.OnLightControl += OnLightControl;
            LightManager.OnChangeState += OnLightStateChange;
        }

        private void OnDisable()
        {
            Door.OnRoomSwitching -= SwitchRoom;
            LightManager.OnLightControl -= OnLightControl;
            LightManager.OnChangeState -= OnLightStateChange;
        }

        public Room GetRoom(RoomType roomType)
        {
            return rooms.Find(x => x.RoomType() == roomType);
        }

        public Room GetRoomAtPoint(Vector3 point)
        {
            return rooms.Find(x => x.ContainsPoint(point));
        }

        private void OnLightStateChange(LightData data)
        {
            if (currentRoom != null)
            {
                currentRoom.ControlLightState(data.State);
            }
        }

        private void OnLightControl(bool turnOn, float duration)
        {
            StartCoroutine(WaitForLights(turnOn, duration));
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
            StartCoroutine(TransitionRooms(roomType, transitionWaitTime, roomSwitchedCallback));
        }

        private IEnumerator WaitForRoomLoad(RoomType roomType, Ref<Room> targetRoom, float transitionWaitTime = -1.0f,
            Action roomSwitchedCallback = null)
        {
            // string currentScene = currentRoom.gameObject.scene.path;
            _roomTypeToRoomConfig.TryGetValue(roomType, out RoomConfig roomConfig);

            bool isLoadFinished = false;
            if (roomConfig != null && roomConfig.owningScene != null)
            {
                SceneLoaderManager.Instance.LoadSceneAsync(roomConfig.owningScene.SceneName, false,
                    _ => isLoadFinished = true);
            }

            Debug.Log("Waiting for room " + roomType + " to load...");

            while (!isLoadFinished)
            {
                yield return null;
            }

            Debug.Log("Room " + roomType + " is loaded!");

            targetRoom.Value = SceneManager.GetSceneByName(roomConfig.owningScene.SceneName).GetRootGameObjects()[0]
                .GetComponent<Room>();

            // yield return StartCoroutine(TransitionRooms(roomTarget, transitionWaitTime, roomSwitchedCallback));
            // SceneLoaderManager.Instance.UnloadSceneAsync(currentScene);
        }

        private IEnumerator TransitionRooms(RoomType newRoomType, float roomLoadWaitTimeOverride,
            Action roomSwitchedCallback)
        {
            _switchingInProgress = true;
            string currentScene = currentRoom.gameObject.scene.path;

            if (roomLogging)
            {
                Debug.Log("Switching rooms: " + currentRoom.RoomType() + " -> " + newRoomType);
            }

            Room newRoom = null;
            foreach (Room room in rooms)
            {
                if (room.RoomType() == newRoomType)
                {
                    newRoom = room;
                    break;
                }
            }

            yield return currentRoom.DeactivateRoom(newRoomType);

            StopDoorAmbiances(currentRoom.Doors());

            // If target room cannot be found, load into memory and wait for it to be available.
            if (newRoom == null)
            {
                var loadedRoomRef = new Ref<Room>();
                yield return WaitForRoomLoad(newRoomType, loadedRoomRef, roomLoadWaitTimeOverride,
                    roomSwitchedCallback);

                newRoom = loadedRoomRef.Value;
                newRoom.SetRoomLogging(roomLogging);
            }

            newRoom.PrepareRoom(currentRoom.RoomType());

            yield return new WaitForSecondsRealtime(roomLoadWaitTimeOverride > 0.0f
                ? roomLoadWaitTimeOverride
                : roomLoadWaitTime);

            newRoom.ActivateRoom(currentRoom.RoomType());

            SceneLoaderManager.Instance.UnloadSceneAsync(currentScene);

            PlayDoorAmbiances(newRoom.Doors());

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

                var roomAmbiences = _roomTypeToRoomConfig[door.GetConnectingRoom()].roomAmbiences;

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

                var roomAmbiences = _roomTypeToRoomConfig[door.GetConnectingRoom()].roomAmbiences;

                foreach (RoomAmbience roomAmbience in roomAmbiences)
                {
                    if (roomAmbience.playInConnectingRooms)
                    {
                        roomAmbience.audio.StopAll();
                    }
                }
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _roomTypeToRoomConfig = LoadAllRooms();
        }

        private static SerializedDictionary<RoomType, RoomConfig> LoadAllRooms()
        {
            string[] guids =
                AssetDatabase.FindAssets("t:" + typeof(RoomConfig), new[] { "Assets/Scriptable Objects/Rooms" });
            int count = guids.Length;
            var dict = new SerializedDictionary<RoomType, RoomConfig>();

            for (int n = 0; n < count; n++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[n]);
                RoomConfig roomConfig = AssetDatabase.LoadAssetAtPath<RoomConfig>(path);
                dict[roomConfig.roomType] = roomConfig;
            }

            return dict;
        }

        public static event Action<Collider2D> OnPlayerSwitchingRoomsEditor;

        private void PlayerToRoom(RoomType roomType)
        {
            GameObject player = GameState.Instance.GetPlayer;
            if (player == null)
            {
                Debug.LogError("ERROR: Player reference not found in GameState!");
                return;
            }

            bool roomFound = false;
            foreach (Room room in rooms)
            {
                if (room.RoomType() == roomType)
                {
                    // Set player to spawn point of first door in the room's list.
                    player.transform.position = room.DoorSpawnPoint(0);
                    OnPlayerSwitchingRoomsEditor?.Invoke(room.CameraBounds());

                    roomFound = true;
                    break;
                }
            }

            if (!roomFound)
            {
                Debug.LogError("ERROR: Room not found!");
            }
        }

        [CustomEditor(typeof(RoomManager))]
        public class RoomManagerEditor : Editor
        {
            private SerializedProperty _roomLoadWaitTimeProp;
            private SerializedProperty _roomLoggingProp;

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
                _roomLoggingProp = serializedObject.FindProperty(nameof(roomLogging));

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

                EditorGUILayout.PropertyField(_roomLoggingProp);

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

                foreach (Door door in currentRoom.Doors())
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