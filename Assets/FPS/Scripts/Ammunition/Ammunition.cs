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
        protected GameObject _explosion;

        protected float _currentDamage;
        protected float _initiationTime;
        protected Transform _rootParent;

        protected abstract float Damage { get; }
        protected abstract float TimeToRelease { get; }

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

            foreach (var trailRenderer in GetComponentsInChildren<TrailRenderer>(true).Union(GetComponents<TrailRenderer>()))
            {
                trailRenderer.enabled = visible;
            }
        }

        public virtual void Prepare(Transform firePoint)
        {
            Position = firePoint.position;
            Rotation = firePoint.rotation;
            Transform.parent = firePoint;

            if (Collider != null)
            {
                Collider.enabled = true;
            }
            if (Renderer != null)
            {
                Renderer.enabled = true;
            }
        }

        public void Initialize(Vector3 force,Transform rootParent, float damageMult = 1f)
        {
            IsVisible = true;
            Invoke("Release", TimeToRelease);
            _rootParent = rootParent;
            _currentDamage = Damage * damageMult;
            Rigidbody.AddForce(force);
            _initiationTime = Time.time;
        }

        protected virtual void SetDamage(IDamagable target,Vector3 source)
        {
            if (target == null)
                return;
            
            target.GetDamage(_currentDamage, source);
        }

        public GameObject GetInstance()
        {
            return InstanceObject;
        }

        public void Release()
        {
            if (IsVisible)
            {
                if (_explosion != null)
                {
                    Instantiate(_explosion, Position, Rotation);
                }

                IsVisible = false;
                Main.Instance.ObjectPool.ReleasePoolable(this);
                CancelInvoke();
            }
        }

        protected abstract void OnCollisionEnter(Collision collision);
    }
}