using AYellowpaper.SerializedCollections;
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

public class Room : MonoBehaviour
{
    [SerializeField] private RoomType roomType;
    [field: SerializeField] public CompositeCollider2D CameraBounds { get; private set; }
    [SerializeField] private SerializedDictionary<RoomType, Transform> roomDoors;
}