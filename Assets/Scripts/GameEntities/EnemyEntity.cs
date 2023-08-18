using UnityEngine;

namespace GameEntities
{
    public class EnemyEntity : MonoBehaviour, IEntity
    {
        [SerializeField] private int maxHP = 100;
        [SerializeField] private int dmgPerHit = 50;
        [SerializeField] [Range(0, 5f)] private float hitCoolDown = 0.3f;

        private int currentHP;
        // private int hitTimer;

        private void Awake()
        {
            currentHP = maxHP;
            // hitTimer = 0;
        }

        public void Died()
        {
            Debug.Log("Enemy Dead");
            gameObject.SetActive(false);
        }

        public void TakeHit(int dmgTaken)
        {
            currentHP -= dmgTaken;

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
    }
}