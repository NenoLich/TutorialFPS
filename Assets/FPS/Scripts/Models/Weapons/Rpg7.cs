using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Models
{
    public class Rpg7 : WeaponModel
    {
        protected override float Force
        {
            get
            {
                return _force == 0f ? _force = 2500f : _force;
            }
        }

        public override int MaxMagazine
        {
            get
            {
                return _maxMagazine == 0 ? _maxMagazine = 1 : _maxMagazine;
            }
        }

        public override float FireRate
        {
            get
            {
                return _fireRate == 0f ? _fireRate = 1f : _fireRate;
            }
        }

        protected override float ReloadTime
        {
            get
            {
                return _reloadTime == 0f ? _reloadTime = 2f : _reloadTime;
            }
        }

        public override void AlternateFire()
        {
            if (Time.time - _lastShotTime < FireRate || _reload)
                return;

            if (Magazine == 0)
            {
                Reload();
                return;
            }

            Magazine--;

            _preparedAmmunition.Initialize(FirePoint.forward * Force,Transform.root);
            _preparedAmmunition = null;

            if (Magazine == 0)
            {
                Reload();
            }
            else
            {
                PrepareAmmo();
            }

            _lastShotTime = Time.time;
        }
    }
}
