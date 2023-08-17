
using Controllers;
using UnityEngine;

namespace Capabilities
{
    [RequireComponent(typeof(Controller)), RequireComponent(typeof(PlayerEntity))]
    public class Shoot : MonoBehaviour
    {
        [SerializeField] Vector3 gunOffset = new Vector2(0.6f, 0);
        [SerializeField, Range(0, 100f)] private float gunRange = 10f;
        [SerializeField] LayerMask layerToHit;


        private Controller _controller;

        private Ray gunRay;
        private Vector2 _direction = Vector2.zero;
        
        private bool facingRight;
        private bool gunActive;
        private float timeBetweenShots;
        private int dmgPerHit;
        private float coolDown;
        

        private void Awake()
        {
            _controller = GetComponent<Controller>();
            dmgPerHit = GetComponent<PlayerEntity>().GetDmg();
            coolDown = GetComponent<PlayerEntity>().GetHitCoolDown();

            gunActive = true;
            timeBetweenShots = coolDown;
            gunRay.origin = transform.position + gunOffset;
            facingRight = true;
            gunRay.direction = transform.right;
        }
        private void Update()
        {
            timeBetweenShots += Time.deltaTime;
            Debug.DrawRay(gunRay.origin, gunRay.direction * gunRange, Color.white);
            if (gunActive && timeBetweenShots >= coolDown)
            {
                if (_controller.input.RetrieveShootInput())
                {
                    Debug.Log("Shoot");
                    gunRay.origin = gameObject.transform.position + gunOffset;
                    
                    RaycastHit hitInfo;
                    bool isHit = Physics.Raycast(gunRay, out hitInfo, gunRange, layerToHit);
                    if (isHit)
                    {
                        hitInfo.transform.gameObject.GetComponent<IEntity>()?.TakeHit(dmgPerHit);
                    }
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
            gunOffset.x *= -1;

            if (facingRight)
            {
                gunRay.direction = gameObject.transform.right * -1;
            }
            else
            {
                gunRay.direction = gameObject.transform.right;
            }
            facingRight = !facingRight;

        }
    }
}

