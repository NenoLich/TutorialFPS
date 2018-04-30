using System;
using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS
{
    public class Bullet : Ammunition
    {
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

        protected override float DamageReductionMultiplier
        {
            get
            {
                return _damageReductionMultiplier == 0 ? _damageReductionMultiplier = 4f : _damageReductionMultiplier;
            }
        }
    }
}