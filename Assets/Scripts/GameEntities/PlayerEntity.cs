using System;
using UnityEngine;

namespace GameEntities
{
    public class PlayerEntity : MonoBehaviour, IEntity
    {
        [SerializeField] private int maxHP = 100;
        [SerializeField] private int dmgPerHit = 50;
        [SerializeField] private float hitCoolDown = 0.2f;

        private float currentHP;
        private bool isAlive = true;

        public static event Action PlayerDeathEvent = delegate {};

        private void OnEnable()
        {
            currentHP = maxHP;
        }

        private void Awake()
        {
            currentHP = maxHP;
        }

        public void Died()
        {
            Debug.Log("PlayerDead");
            isAlive = false;
            PlayerDeathEvent?.Invoke();
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
        public bool IsAlive()
        {
            return isAlive;
        }
    }
}