using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField, Range(0, 10f)] private float maxTimeActive = 4f;
    [SerializeField, Range(0, 100f)] private float speed = 10f;

    private float timeActive;

    private void OnEnable()
    {
        timeActive = 0f;
    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        timeActive += Time.deltaTime;

        if (timeActive > maxTimeActive)
            ObjectPooler.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            ObjectPooler.Destroy(gameObject);
        }
    }

}
