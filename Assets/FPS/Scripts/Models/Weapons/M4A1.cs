using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Models
{
    public class M4A1 : WeaponModel
    {
        protected override float Force
        {
            get
            {
                return _force == 0f ? _force = 1000f : _force;
            }
        }

        public override int MaxMagazine
        {
            get
            {
                return _maxMagazine == 0 ? _maxMagazine = 30 : _maxMagazine;
            }
        }

        public override float FireRate
        {
            get
            {
                return _fireRate == 0f ? _fireRate = 0.25f : _fireRate;
            }
        }

        protected override float ReloadTime
        {
            get
            {
                return _reloadTime == 0f ? _reloadTime = 1.75f : _reloadTime;
            }
        }
    }
}