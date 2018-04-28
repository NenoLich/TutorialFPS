using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS
{
    public class Rpg7 : Weapon
    {
        protected override float Force
        {
            get
            {
                return _force == 0f ? _force = 500f : _force;
            }
        }

        protected override int MaxMagazine
        {
            get
            {
                return _maxMagazine == 0 ? _maxMagazine = 1 : _maxMagazine;
            }
        }

        protected override float FireRate
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

        public override void Fire()
        {
            base.Fire();

            Missile missile = Main.Instance.ObjectPool.AcquirePoolable<Missile>();
            missile.Position = FirePoint.position;
            missile.Rotation = FirePoint.rotation;
            missile.Initialize(FirePoint.forward * Force);
        }
    }
}
