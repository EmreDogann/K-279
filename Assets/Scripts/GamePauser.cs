using Events;
using UnityEngine;

public class GamePauser : MonoBehaviour
{
    [SerializeField] private BoolEventListener pauseListener;

    private void OnEnable()
    {
        pauseListener.Response.AddListener(OnPauseToggle);
    }

    private void OnDisable()
    {
        pauseListener.Response.RemoveListener(OnPauseToggle);
    }

    private void OnPauseToggle(bool isPaused)
    {
        if (isPaused)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    public void Unpause(float timeScale = 1.0f)
    {
        Time.timeScale = timeScale;
    }

    public void Pause(float timeScale = 0.0f)
    {
        Time.timeScale = timeScale;
    }
}