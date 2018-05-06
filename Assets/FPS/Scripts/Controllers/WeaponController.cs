using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Models;
using TutorialFPS.Services;
using TutorialFPS.Views;
using UnityEngine;

namespace TutorialFPS.Controllers
{
    public class WeaponController : BaseController
    {
        [SerializeField] private float _minTimeBetweenScroll = 0.3f;
        private int _currentWeaponId;
        private int _currentAmmoId;
        private int _previousWeaponId;
        private float _lastScrollTime;

        private void Start()
        {
            for (int i = 1; i < Main.Instance.ObjectManager.Weapons.Length; i++)
                Main.Instance.ObjectManager.Weapons[i].IsVisible = false;
        }

        public void ChangeWeapon(float deltaScroll)
        {
            if (Time.time - _lastScrollTime < _minTimeBetweenScroll)
            {
                return;
            }

            Main.Instance.ObjectManager.Weapons[_currentWeaponId].IsVisible = false;

            _currentWeaponId = deltaScroll < 0 ? _currentWeaponId + 1 : _currentWeaponId - 1;

            if (_currentWeaponId >= Main.Instance.ObjectManager.Weapons.Length)
            {
                _currentWeaponId = 0;
            }
            else if (_currentWeaponId < 0)
            {
                _currentWeaponId = Main.Instance.ObjectManager.Weapons.Length - 1;
            }

            Main.Instance.ObjectManager.Weapons[_currentWeaponId].IsVisible = true;

            _lastScrollTime = Time.time;
        }

        public void SwitchWeapon()
        {
            Main.Instance.ObjectManager.Weapons[_currentWeaponId].IsVisible = false;

            int temp = _previousWeaponId;
            _previousWeaponId = _currentWeaponId;
            _currentWeaponId = temp;

            Main.Instance.ObjectManager.Weapons[_currentWeaponId].IsVisible = true;
        }

        public void Fire()
        {
            Main.Instance.ObjectManager.Weapons[_currentWeaponId].weaponModel.Fire();
        }

        public void AlternateFire()
        {
            Main.Instance.ObjectManager.Weapons[_currentWeaponId].weaponModel.AlternateFire();
        }

        public void Reload()
        {
            Main.Instance.ObjectManager.Weapons[_currentWeaponId].weaponModel.Reload();
        }

        public override void OnNotification(Notification notification, Object target, params object[] data)
        {
            base.OnNotification(notification, target, data);

            switch (notification)
            {
                case Notification.WeaponMagazineChanged:
                    if ((WeaponModel)target== Main.Instance.ObjectManager.Weapons[_currentWeaponId].weaponModel)
                    {
                        Main.Instance.ObjectManager.Weapons[_currentWeaponId].weaponView.SetMagazineView(
                            Main.Instance.ObjectManager.Weapons[_currentWeaponId].weaponModel.Magazine,
                            Main.Instance.ObjectManager.Weapons[_currentWeaponId].weaponModel.MaxMagazine);
                    }
                    break;
            }
                
        }
    }
}
