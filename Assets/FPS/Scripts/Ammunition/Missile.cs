using System;
using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS
{
    public class Missile : Ammunition
    {
        [SerializeField]
        private float _explosionRadius;
        [SerializeField]
        private float _explosionForce;

        protected override float Damage
        {
            get
            {
                return _damage == 0 ? _damage = 100f : _damage;
            }
        }

        protected override float TimeToRelease
        {
            get
            {
                return _timeToRelease == 0 ? _timeToRelease = 5f : _timeToRelease;
            }
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Bullet"||collision.transform.root==_rootParent)
            {
                return;
            }

            foreach (Collider coll in Physics.OverlapSphere(Position, _explosionRadius))
            {
                Rigidbody _rigidbody = coll.GetComponent<Rigidbody>();
                if (_rigidbody!=null)
                {
                    _rigidbody.AddExplosionForce(_explosionForce,Position,_explosionRadius);
                }

                IDamagable iDamagable = coll.GetComponent<IDamagable>();
                if (iDamagable!=null)
                {
                    _currentDamage = Damage * (_explosionRadius - (coll.ClosestPoint(Position) - Position).magnitude) /
                                     _explosionRadius;

                    SetDamage(iDamagable);
                }
            }

            Release();
        }
    }
}
