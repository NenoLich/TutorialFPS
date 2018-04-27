using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS
{
    public class Pistol : Weapon
    {
        protected override float Force
        {
            get
            {
                return _force == 0f ? _force = 350f : _force;
            }
        }

        protected override int MaxMagazine
        {
            get
            {
                return _maxMagazine == 0 ? _maxMagazine = 6 : _maxMagazine;
            }
        }

        protected override float FireRate
        {
            get
            {
                return _fireRate == 0f ? _fireRate = 0.85f : _fireRate;
            }
        }

        protected override float ReloadTime
        {
            get
            {
                return _reloadTime == 0f ? _reloadTime = 1.5f : _reloadTime;
            }
        }

        public override void Fire()
        {
            if (Time.time - _lastShotTime < FireRate || _reload)
                return;

            base.Fire();

            Bullet bullet = Main.Instance.ObjectPool.AcquireBullet();
            bullet.Position = FirePoint.position;
            bullet.Rotation = FirePoint.rotation;
            bullet.Initialize(FirePoint.forward * Force);

            _lastShotTime = Time.time;
        }
    }
}
