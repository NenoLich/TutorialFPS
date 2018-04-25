using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS
{
    public class Bullet : Ammunition
    {
        [SerializeField]
        private float _timeToDestruct = 5f;

        private float Damage
        {
            get
            {
                return _damage == 0 ? _damage = 20f : _damage;
            }
        }

        private float Mass
        {
            get
            {
                return _mass == 0 ? _mass = 0.01f : _mass;
            }
        }

        public void Initialize(Vector3 force, float damageMult = 1f)
        {
            _currentDamage = Damage * damageMult;
            Invoke("Release", _timeToDestruct);
            Rigidbody.mass = Mass;

            Rigidbody.AddForce(force);
        }

        private void Release()
        {
            Main.Instance.ObjectPool.ReleaseBullet(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Bullet") return;

            SetDamage(collision.collider.GetComponent<IDamagable>());
            Release();
        }
    }
}