using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class AudioListenerSettings : MonoBehaviour
{
    [SerializeField] private AudioVelocityUpdateMode velocityUpdateMode;

    private AudioListener _audioListener;

    private void Awake()
    {
        _audioListener = GetComponent<AudioListener>();
        _audioListener.velocityUpdateMode = velocityUpdateMode;
    }
}