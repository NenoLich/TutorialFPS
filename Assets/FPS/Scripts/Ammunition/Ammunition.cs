using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS
{
    public abstract class Ammunition : BaseGameObject, IPoolable
    {
        [SerializeField]
        protected float _damage;
        [SerializeField]
        protected float _timeToRelease;
        [SerializeField]
        protected float _damageReductionMultiplier;

        protected float _currentDamage;
        protected float _initiationTime;

        protected abstract float Damage { get; }
        protected abstract float TimeToRelease { get; }
        protected abstract float DamageReductionMultiplier { get; }

        protected override void Awake()
        {
            base.Awake();

            IsVisible = false;
        }

        protected override void SetVisibility(Transform objTransform, bool visible)
        {
            base.SetVisibility(objTransform, visible);

            foreach (var r in GetComponentsInChildren<Rigidbody>(true).Union(GetComponents<Rigidbody>()))
                if (visible)
                {
                    r.isKinematic = false;
                    r.detectCollisions = true;
                }
                else
                {
                    r.isKinematic = true;
                    r.detectCollisions = false;
                    r.velocity = Vector3.zero;
                }

        }

        public virtual void Prepare(Transform firePoint)
        {
            Position = firePoint.position;
            Rotation = firePoint.rotation;
            Transform.parent = firePoint;
        }

        public void Initialize(Vector3 force, float damageMult = 1f)
        {
            IsVisible = true;
            Invoke("Release", TimeToRelease);
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

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Player" || collision.collider.tag == "Bullet")
            {
                return;
            }

            _currentDamage = Damage * (1 - (Time.time - _initiationTime) * DamageReductionMultiplier);
            SetDamage(collision.collider.GetComponent<IDamagable>());

            Release();
        }
    }
}