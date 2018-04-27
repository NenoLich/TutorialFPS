using System;
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
        [SerializeField]
        private float _trailTime = 0.05f;

        private float _initiationTime;

        private TrailRenderer _trailRenderer;

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

        protected override void SetVisibility(Transform objTransform, bool visible)
        {
            base.SetVisibility(objTransform, visible);
            _trailRenderer.time = visible? _trailTime:0f;
        }

        protected override void Awake()
        {
            base.Awake();
            _trailRenderer = GetComponentInChildren<TrailRenderer>(true);
            IsVisible = false;
        }

        public void Initialize(Vector3 force, float damageMult = 1f)
        {
            IsVisible = true;
            _currentDamage = Damage * damageMult;
            Invoke("Release", _timeToDestruct);

            Rigidbody.mass = Mass;
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(force);
            _initiationTime = Time.time;
        }

        private void Release()
        {
            if (IsVisible)
            {
                IsVisible = false;
                Main.Instance.ObjectPool.ReleaseBullet(this);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Player" || collision.collider.tag == "Bullet")
            {
                return;
            }

            _currentDamage = Damage * (1 - (Time.time - _initiationTime) * 4);
            SetDamage(collision.collider.GetComponent<IDamagable>());

            Release();
        }
    }
}