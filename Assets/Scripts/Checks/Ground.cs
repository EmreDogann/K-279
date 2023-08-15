using UnityEngine;

namespace Checks
{
    public class Ground : MonoBehaviour
    {
        public bool OnGround { get; private set; }
        public float Friction { get; private set; }

        private Vector2 _normal;
        private PhysicMaterial _material;

        private void OnCollisionExit(Collision collision)
        {
            OnGround = false;
            Friction = 0;
        }

        private void OnCollisionEnter(Collision collision)
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }

        private void EvaluateCollision(Collision collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                _normal = collision.GetContact(i).normal;
                OnGround |= _normal.y >= 0.9f;
            }
        }

        private void RetrieveFriction(Collision collision)
        {
            _material = collision.collider.sharedMaterial;

            Friction = 0;

            if (_material != null)
            {
                Friction = _material.staticFriction;
            }
        }
    }
}