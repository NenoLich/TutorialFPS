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
        protected float _damageReductionMultiplier;

        protected override float Damage
        {
            get
            {
                return _damage == 0 ? _damage = 20f : _damage;
            }
        }

        protected override float TimeToRelease
        {
            get
            {
                return _timeToRelease == 0 ? _timeToRelease = 5f : _timeToRelease;
            }
        }

        protected float DamageReductionMultiplier
        {
            get
            {
                return _damageReductionMultiplier == 0 ? _damageReductionMultiplier = 4f : _damageReductionMultiplier;
            }
        }

        protected override void OnCollisionEnter(Collision collision)
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