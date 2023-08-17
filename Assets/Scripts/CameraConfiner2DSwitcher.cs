using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineConfiner2D))]
public class CameraConfiner2DSwitcher : RoomSwitchListener
{
    private CinemachineConfiner2D _confiner2D;

    private void Awake()
    {
        _confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    public override void OnDisable()
    {
        base.OnDisable();

        _confiner2D.m_BoundingShape2D = null;
        _confiner2D.InvalidateCache();
    }

    public override void OnRoomActivate(RoomData roomData)
    {
        _confiner2D.m_BoundingShape2D = roomData.CameraBounds;
        _confiner2D.InvalidateCache();
    }
}