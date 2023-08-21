using UnityEngine;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitGameOnInput : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private InputAction _quitAction;

    private void Awake()
    {
        _quitAction = playerInput.actions["Quit"];
    }

    private void Update()
    {
        if (_quitAction.WasPressedThisFrame())
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}