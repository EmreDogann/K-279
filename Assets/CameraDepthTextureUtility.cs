using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraDepthTextureUtility : MonoBehaviour
{
    [SerializeField] private DepthTextureMode depthTextureMode;

    private Camera _cam;

    // Start is called before the first frame update
    private void Awake()
    {
        _cam = GetComponent<Camera>();
        _cam.depthTextureMode |= depthTextureMode;
    }
}