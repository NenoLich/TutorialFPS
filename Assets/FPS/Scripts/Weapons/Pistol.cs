using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS
{
    public class Pistol : Weapon
    {
        [SerializeField]
        private float _force = 100f;
        [SerializeField]
        private Transform _firePoint;
        [SerializeField]
        private float _timeout = 0.5f;
        private float _lastShotTime;

        public override void Fire()
        {
            if (Time.time - _lastShotTime < _timeout)
                return;

            Bullet bullet = Main.Instance.ObjectPool.AcquireBullet();
            bullet.Position = _firePoint.position;
            bullet.Rotation = _firePoint.rotation;
            bullet.Initialize(_firePoint.forward * _force);

            _lastShotTime = Time.time;
        }
    }
}
