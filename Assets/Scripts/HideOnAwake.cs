using UnityEngine;

public class HideOnAwake : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
}