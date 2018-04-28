using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] protected float _force;
        [SerializeField] protected int _maxMagazine;
        [SerializeField] protected float _reloadTime;
        [SerializeField] protected float _fireRate;

        protected float _lastShotTime;
        protected Transform _firePoint;
        protected bool _reload = false;
        protected int _magazine;

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
        }

        protected override void SetVisibility(Transform objTransform, bool visible)
        {
            base.SetVisibility(objTransform, visible);
            _weaponView.gameObject.SetActive(visible);
        }

        public virtual void Fire()
        {
            if (Time.time - _lastShotTime < FireRate || _reload)
                return;

            _magazine--;
            _weaponView.SetMagazineView(_magazine, MaxMagazine);

            if (_magazine == 0)
            {
                Reload();
            }

            _lastShotTime = Time.time;
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
            _weaponView.SetMagazineView(_magazine, MaxMagazine);
            _reload = false;
        }
    }
}
