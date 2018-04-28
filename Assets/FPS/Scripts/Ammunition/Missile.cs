using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS
{
    public class Missile : Ammunition
    {
        protected override float Damage
        {
            get
            {
                return _damage == 0 ? _damage = 100f : _damage;
            }
        }

        protected override float Mass
        {
            get
            {
                return _mass == 0 ? _mass = 10f : _mass;
            }
        }

        protected override float TimeToRelease
        {
            get
            {
                return _timeToRelease == 0 ? _timeToRelease = 5f : _timeToRelease;
            }
        }

        protected override float DamageReductionMultiplier
        {
            get
            {
                return _damageReductionMultiplier == 0 ? _damageReductionMultiplier = 0f : _damageReductionMultiplier;
            }
        }

        protected override void SetVisibility(Transform objTransform, bool visible)
        {
            base.SetVisibility(objTransform, visible);
        }

        protected override void Awake()
        {
            base.Awake();

            IsVisible = false;
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            Release();
        }
    }
}
