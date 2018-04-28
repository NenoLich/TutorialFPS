using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS
{
    public abstract class Ammunition : BaseGameObject, IPoolable
    {
        [SerializeField]
        protected float _damage;
        [SerializeField]
        protected float _mass;
        [SerializeField]
        protected float _timeToRelease;
        [SerializeField]
        protected float _damageReductionMultiplier;

        protected float _currentDamage;
        protected float _initiationTime;

        protected abstract float Mass { get; }
        protected abstract float Damage { get; }
        protected abstract float TimeToRelease { get; }
        protected abstract float DamageReductionMultiplier { get; }

        public void Initialize(Vector3 force, float damageMult = 1f)
        {
            SetDefaults();
            _currentDamage = Damage * damageMult;
            Rigidbody.AddForce(force);
            _initiationTime = Time.time;
        }

        protected virtual void SetDamage(IDamagable target)
        {
            if (target == null)
                return;

            target.GetDamage(_currentDamage);
        }

        public GameObject GetInstance()
        {
            return InstanceObject;
        }

        public void Release()
        {
            if (IsVisible)
            {
                IsVisible = false;
                Main.Instance.ObjectPool.ReleasePoolable(this);
            }
        }

        public void SetDefaults()
        {
            IsVisible = true;
            Invoke("Release", _timeToRelease);
            Rigidbody.mass = Mass;
            Rigidbody.velocity = Vector3.zero;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Player" || collision.collider.tag == "Bullet")
            {
                return;
            }

            _currentDamage = Damage * (1 - (Time.time - _initiationTime) * DamageReductionMultiplier);
            SetDamage(collision.collider.GetComponent<IDamagable>());
        }
    }
}