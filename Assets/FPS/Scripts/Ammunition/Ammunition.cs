using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS
{
    public abstract class Ammunition : BaseGameObject
    {
        [SerializeField]
        protected float _damage;
        [SerializeField]
        protected float _mass;

        protected float _currentDamage;

        protected virtual void SetDamage(IDamagable target)
        {
            if (target == null)
                return;

            target.GetDamage(_currentDamage);
        }

    }
}