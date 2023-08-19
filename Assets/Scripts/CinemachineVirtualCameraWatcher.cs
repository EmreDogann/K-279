using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineVirtualCameraWatcher : MonoBehaviour
{
    public UnityEvent OnCameraLive;
    public UnityEvent OnCameraDeactive;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private bool _isLive;

    private void Awake()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        bool status = CinemachineCore.Instance.IsLive(_cinemachineVirtualCamera);
        if (_isLive != status)
        {
            _isLive = status;
            if (_isLive)
            {
                OnCameraLive?.Invoke();
            }
            else
            {
                OnCameraDeactive?.Invoke();
            }
        }
    }
}