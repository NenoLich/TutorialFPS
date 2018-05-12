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
        private PlayerModel _playerModel;
        private int _previousWeaponId;
        private float _lastScrollTime;

        private void Start()
        {
            _playerModel = Main.Player.GetComponent<PlayerModel>();
            _playerModel.WeaponsMagazine =new int[Main.Instance.ObjectManager.Weapons.Length];

            for (int i = 1; i < Main.Instance.ObjectManager.Weapons.Length; i++)
            {
                Main.Instance.ObjectManager.Weapons[i].IsVisible = false;
                _playerModel.WeaponsMagazine[i] = Main.Instance.ObjectManager.Weapons[i].weaponModel.Magazine;
            }
        }

        public void ChangeWeapon(float deltaScroll)
        {
            if (Time.time - _lastScrollTime < _minTimeBetweenScroll)
            {
                return;
            }

            Main.Instance.ObjectManager.Weapons[_playerModel.CurrentWeaponId].IsVisible = false;

            _playerModel.CurrentWeaponId = deltaScroll < 0 ? _playerModel.CurrentWeaponId + 1 : _playerModel.CurrentWeaponId - 1;

            if (_playerModel.CurrentWeaponId >= Main.Instance.ObjectManager.Weapons.Length)
            {
                _playerModel.CurrentWeaponId = 0;
            }
            else if (_playerModel.CurrentWeaponId < 0)
            {
                _playerModel.CurrentWeaponId = Main.Instance.ObjectManager.Weapons.Length - 1;
            }

            Main.Instance.ObjectManager.Weapons[_playerModel.CurrentWeaponId].IsVisible = true;

            _lastScrollTime = Time.time;
        }

        public void SwitchWeapon()
        {
            Main.Instance.ObjectManager.Weapons[_playerModel.CurrentWeaponId].IsVisible = false;

            int temp = _previousWeaponId;
            _previousWeaponId = _playerModel.CurrentWeaponId;
            _playerModel.CurrentWeaponId = temp;

            Main.Instance.ObjectManager.Weapons[_playerModel.CurrentWeaponId].IsVisible = true;
        }

        public void Fire()
        {
            Main.Instance.ObjectManager.Weapons[_playerModel.CurrentWeaponId].weaponModel.Fire();
        }

        public void AlternateFire()
        {
            Main.Instance.ObjectManager.Weapons[_playerModel.CurrentWeaponId].weaponModel.AlternateFire();
        }

        public void Reload()
        {
            Main.Instance.ObjectManager.Weapons[_playerModel.CurrentWeaponId].weaponModel.Reload();
        }

        private void OnSaveLoaded()
        {
            for (int i = 1; i < Main.Instance.ObjectManager.Weapons.Length; i++)
            {
                Main.Instance.ObjectManager.Weapons[i].IsVisible = false;
                Main.Instance.ObjectManager.Weapons[i].weaponModel.Magazine = _playerModel.WeaponsMagazine[i];
            }

            Main.Instance.ObjectManager.Weapons[_playerModel.CurrentWeaponId].IsVisible = true;
        }

        public override void OnNotification(Notification notification, Object target, params object[] data)
        {
            base.OnNotification(notification, target, data);

            switch (notification)
            {
                case Notification.WeaponMagazineChanged:
                    if ((WeaponModel)target== Main.Instance.ObjectManager.Weapons[_playerModel.CurrentWeaponId].weaponModel)
                    {
                        Main.Instance.ObjectManager.Weapons[_playerModel.CurrentWeaponId].weaponView.SetMagazineView(
                            Main.Instance.ObjectManager.Weapons[_playerModel.CurrentWeaponId].weaponModel.Magazine,
                            Main.Instance.ObjectManager.Weapons[_playerModel.CurrentWeaponId].weaponModel.MaxMagazine);

                        _playerModel.WeaponsMagazine[_playerModel.CurrentWeaponId] = Main.Instance.ObjectManager
                            .Weapons[_playerModel.CurrentWeaponId].weaponModel.Magazine;
                    }
                    break;

                case Notification.SaveLoaded:
                    if ((PlayerModel)target==_playerModel)
                    {
                        OnSaveLoaded();
                    }
                    break;
            }
                
        }
    }
}
