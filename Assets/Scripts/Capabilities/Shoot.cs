
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Shoot : MonoBehaviour
{
    [SerializeField] Vector3 spawnOffset = new Vector2(0.6f, 0);
    [SerializeField, Range(0, 5f)] private float coolDown = 0.2f;

    private Vector2 _direction = Vector2.zero;
    private bool facingRight = true;
    private bool gunActive;
    private float timeBetweenShots;
    private Controller _controller;

    public Quaternion gunDirection;

    private void Awake()
    {
        _controller = GetComponent<Controller>();

        gunActive = true;
        timeBetweenShots = 0f;
        gunDirection = Quaternion.identity;
    }
    private void Update()
    {
        timeBetweenShots += Time.deltaTime;
        
        if (gunActive && timeBetweenShots >= coolDown) 
        {
            if (_controller.input.RetrieveShootInput())
            {
                Vector2 bulletSpawnPosition = gameObject.transform.position + spawnOffset;
                ObjectPooler.Generate("Bullet", bulletSpawnPosition, gunDirection);
            }
        }
        
    }

    private void FixedUpdate()
    {
        _direction.x = _controller.input.RetrieveMoveInput(gameObject);
        if (_direction.x > 0f && !facingRight)
        {
            FlipGun();
        }
        else if (_direction.x < 0f && facingRight)
        {
            FlipGun();
        }
    }

    private void FlipGun()
    {
        spawnOffset.x *= -1;

        if (facingRight)
        {
            gunDirection.eulerAngles = new Vector3(0, 0, 180);
        } else
        {
            gunDirection.eulerAngles = new Vector3(0, 0, 0);
        }
        facingRight = !facingRight;

    }
}
