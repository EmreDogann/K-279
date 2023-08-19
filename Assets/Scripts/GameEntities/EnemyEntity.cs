using MyBox;
using System.Collections;
using UnityEngine;

namespace GameEntities
{
    public class EnemyEntity : MonoBehaviour, IEntity
    {
        [SerializeField] private int maxHP = 100;
        [SerializeField] private int dmgPerHit = 50;
        [SerializeField] [Range(0, 5f)] private float hitCoolDown = 0.3f;
        [SerializeField, Range(0, 5f)] private float deathInvisibleDelay = 0.5f;
        [SerializeField] GameObject droppedItem;
        
        [Separator("Animation")]
        [SerializeField] private Animator _animator;

        private bool itemAlreadyDropped = false;
        private bool isAlive = true;
        private int currentHP;
        private float deathInvisibleTimer;
        private static readonly int DeathState = Animator.StringToHash("EnemyDeath");
        private static readonly int HurtState = Animator.StringToHash("EnemyHurt");
        // private int hitTimer;

        private void Awake()
        {
            currentHP = maxHP;
            // hitTimer = 0;
        }

        public void Died()
        {
            if (!itemAlreadyDropped) 
            {
                itemAlreadyDropped = true;
                GameObject item = Instantiate(droppedItem, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                item.SetActive(true);
            }
            Debug.Log("Enemy Dead");
            isAlive = false;
            _animator.SetTrigger(DeathState);
            StartCoroutine(DeathInvisibleDelay());
        }

        public void TakeHit(int dmgTaken)
        {
            currentHP -= dmgTaken;
            _animator.SetTrigger(HurtState);
            if (currentHP < 0)
            {
                
                Died();
            }
        }

        public int GetDmg()
        {
            return dmgPerHit;
        }

        public float GetHitCoolDown()
        {
            return hitCoolDown;
        }
        public bool IsAlive()
        {
            return isAlive;
        }
        IEnumerator DeathInvisibleDelay()
        {
            while(deathInvisibleTimer < deathInvisibleDelay)
            {
                deathInvisibleTimer += Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}