using System;
using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using TutorialFPS.Services;
using TutorialFPS.Views;
using UnityEngine;

namespace TutorialFPS
{
    /// <summary>
    /// Базовый класс для всех типов оружий
    /// </summary>
    public abstract class Weapon : BaseGameObject
    {
        [SerializeField] protected WeaponView _weaponView;
        [SerializeField] protected Ammunition _ammoType;
        [SerializeField] protected float _force;
        [SerializeField] protected int _maxMagazine;
        [SerializeField] protected float _reloadTime;
        [SerializeField] protected float _fireRate;

        protected float _lastShotTime;
        protected Transform _firePoint;
        protected bool _reload = false;
        protected int _magazine;
        protected Ammunition _preparedAmmunition;

        protected Transform FirePoint
        {
            get
            {
                if ((object)_firePoint == null)
                {
                    _firePoint = Transform.Find("FirePoint");
                }

                return _firePoint;
            }
        }

        protected abstract float Force { get; }
        protected abstract int MaxMagazine { get; }
        protected abstract float FireRate { get; }
        protected abstract float ReloadTime { get; }

        protected override void Awake()
        {
            base.Awake();
            _magazine = MaxMagazine;
            PrepareAmmo();
        }

        protected override void SetVisibility(Transform objTransform, bool visible)
        {
            base.SetVisibility(objTransform, visible);

            if (_weaponView != null)
            {
                _weaponView.gameObject.SetActive(visible);
            }
        }

        public virtual void Fire()
        {
            if (Time.time - _lastShotTime < FireRate || _reload)
                return;

            _magazine--;
            if (_weaponView != null)
            {
                _weaponView.SetMagazineView(_magazine, MaxMagazine);

            }

            _preparedAmmunition.Initialize(FirePoint.forward * Force);
            _preparedAmmunition.Transform.parent = null;

            if (_magazine == 0)
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
            if (_magazine == MaxMagazine)
            {
                return;
            }

            StartCoroutine(OnReload());
        }

        private IEnumerator OnReload()
        {
            _reload = true;
            yield return new WaitForSeconds(ReloadTime);

            _magazine = MaxMagazine;

            if (_weaponView != null)
            {
                _weaponView.SetMagazineView(_magazine, MaxMagazine);

            }

            PrepareAmmo();
            _reload = false;
        }

        protected void PrepareAmmo()
        {
            _preparedAmmunition = (Ammunition)Main.Instance.ObjectPool.AcquirePoolable(_ammoType.GetType());
            _preparedAmmunition.Prepare(FirePoint);
        }
    }
}
