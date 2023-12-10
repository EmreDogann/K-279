using UnityEngine;

public class GameState : MonoBehaviour
{
    private static readonly object Padlock = new object();
    public static GameState Instance { get; private set; }
    public GameObject GetPlayer { get; private set; }

    private void Awake()
    {
        lock (Padlock)
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void RegisterPlayer(GameObject player)
    {
        if (player.CompareTag("Player"))
        {
            GetPlayer = player;
        }
        else
        {
            Debug.LogWarning(
                "WARNING: RegisterPlayer failed. Provided GameObject does not have the <color=green>\"Player\"</color> tag.");
        }
    }
}