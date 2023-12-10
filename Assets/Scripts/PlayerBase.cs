using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private void Awake()
    {
        GameState.Instance.RegisterPlayer(gameObject);
    }
}