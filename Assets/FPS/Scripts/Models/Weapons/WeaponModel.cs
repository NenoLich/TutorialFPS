using System;
using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using TutorialFPS.Services;
using TutorialFPS.Views;
using UnityEngine;

namespace TutorialFPS.Models
{
    /// <summary>
    /// Базовый класс для всех типов оружий
    /// </summary>
    public abstract class WeaponModel : BaseGameObject
    {
        [HideInInspector] public bool _reload = false;

        [SerializeField] protected Ammunition _ammoType;
        [SerializeField] protected float _force;
        [SerializeField] protected int _maxMagazine;
        [SerializeField] protected float _reloadTime;
        [SerializeField] protected float _fireRate;

        protected float _lastShotTime;
        protected Transform _firePoint;
        protected int _magazine;
        protected Ammunition _preparedAmmunition;

        protected Transform FirePoint
        {
            get
            {
                if (_firePoint == null)
                {
                    _firePoint = Transform.Find("FirePoint");
                }

                return _firePoint;
            }
        }

        public int Magazine
        {
            get { return _magazine; }
            protected set
            {
                _magazine = value;
                Main.Instance.Notify(Notification.WeaponMagazineChanged, this);
            }
        }

        protected abstract float Force { get; }
        public abstract int MaxMagazine { get; }
        public abstract float FireRate { get; }
        protected abstract float ReloadTime { get; }

        protected override void Awake()
        {
            base.Awake();
            Magazine = MaxMagazine;
            PrepareAmmo();
        }

        protected override void SetVisibility(Transform objTransform, bool visible)
        {
            base.SetVisibility(objTransform, visible);

            if (_reload)
            {
                return;
            }

            PrepareAmmo();
        }

        public virtual void Fire()
        {
            if (Time.time - _lastShotTime < FireRate || _reload)
                return;

            Magazine--;

            _preparedAmmunition.Initialize(FirePoint.forward * Force, Transform.root);
            _preparedAmmunition.Transform.parent = null;
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

        public virtual void AlternateFire()
        {

        }

        public void Reload()
        {
            if (Magazine == MaxMagazine)
            {
                return;
            }

            StartCoroutine(OnReload());
        }

        private IEnumerator OnReload()
        {
            _reload = true;
            yield return new WaitForSeconds(ReloadTime);

            Magazine = MaxMagazine;

            PrepareAmmo();

            _reload = false;
        }

        protected void PrepareAmmo()
        {
            if (!IsVisible || _preparedAmmunition != null)
            {
                return;
            }

            _preparedAmmunition = (Ammunition)Main.Instance.ObjectPool.AcquirePoolable(_ammoType.GetType());
            _preparedAmmunition.Prepare(FirePoint);
        }
    }
}
