using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
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

        private PlayerModel PlayerModel
        {
            get
            {
                if (_playerModel == null)
                {
                    _playerModel = Main.Instance.PlayerModel;
                }

                return _playerModel;
            }
        }

        private void Start()
        {
            PlayerModel.WeaponsMagazine = new int[Main.Instance.ObjectManager.Weapons.Length];

            for (int i = 0; i < Main.Instance.ObjectManager.Weapons.Length; i++)
            {
                Main.Instance.ObjectManager.Weapons[i].IsVisible = false;
                PlayerModel.WeaponsMagazine[i] = Main.Instance.ObjectManager.Weapons[i].weaponModel.Magazine;
            }

            Main.Instance.ObjectManager.Weapons[0].IsVisible = true;
        }

        public void ChangeWeapon(float deltaScroll)
        {
            if (Time.time - _lastScrollTime < _minTimeBetweenScroll)
            {
                return;
            }

            Main.Instance.ObjectManager.Weapons[PlayerModel.CurrentWeaponId].IsVisible = false;

            PlayerModel.CurrentWeaponId = deltaScroll < 0 ? PlayerModel.CurrentWeaponId + 1 : PlayerModel.CurrentWeaponId - 1;

            if (PlayerModel.CurrentWeaponId >= Main.Instance.ObjectManager.Weapons.Length)
            {
                PlayerModel.CurrentWeaponId = 0;
            }
            else if (PlayerModel.CurrentWeaponId < 0)
            {
                PlayerModel.CurrentWeaponId = Main.Instance.ObjectManager.Weapons.Length - 1;
            }

            Main.Instance.ObjectManager.Weapons[PlayerModel.CurrentWeaponId].IsVisible = true;

            _lastScrollTime = Time.time;
        }

        public void SwitchWeapon()
        {
            Main.Instance.ObjectManager.Weapons[PlayerModel.CurrentWeaponId].IsVisible = false;

            int temp = _previousWeaponId;
            _previousWeaponId = PlayerModel.CurrentWeaponId;
            PlayerModel.CurrentWeaponId = temp;

            Main.Instance.ObjectManager.Weapons[PlayerModel.CurrentWeaponId].IsVisible = true;
        }

        public void Fire()
        {
            Main.Instance.ObjectManager.Weapons[PlayerModel.CurrentWeaponId].weaponModel.Fire();
        }

        public void AlternateFire()
        {
            Main.Instance.ObjectManager.Weapons[PlayerModel.CurrentWeaponId].weaponModel.AlternateFire();
        }

        public void Reload()
        {
            Main.Instance.ObjectManager.Weapons[PlayerModel.CurrentWeaponId].weaponModel.Reload();
        }

        private void OnSaveLoaded()
        {
            for (int i = 0; i < Main.Instance.ObjectManager.Weapons.Length; i++)
            {
                Main.Instance.ObjectManager.Weapons[i].weaponModel.Magazine = PlayerModel.WeaponsMagazine[i];
                Main.Instance.ObjectManager.Weapons[i].IsVisible = false;
            }
            
            Main.Instance.ObjectManager.Weapons[PlayerModel.CurrentWeaponId].IsVisible = true;
        }

        public override void OnNotification(Notification notification, Object target, params object[] data)
        {
            base.OnNotification(notification, target, data);

            switch (notification)
            {
                case Notification.WeaponMagazineChanged:
                    for (int i = 0; i < Main.Instance.ObjectManager.Weapons.Length; i++)
                    {
                        if ((WeaponModel)target == Main.Instance.ObjectManager.Weapons[i].weaponModel)
                        {
                            Main.Instance.ObjectManager.Weapons[i].weaponView.SetMagazineView(
                                Main.Instance.ObjectManager.Weapons[i].weaponModel.Magazine,
                                Main.Instance.ObjectManager.Weapons[i].weaponModel.MaxMagazine);

                            _playerModel.WeaponsMagazine[i] = Main.Instance.ObjectManager
                                .Weapons[i].weaponModel.Magazine;
                        }
                    }

                    break;

                case Notification.SaveLoaded:
                    if ((PlayerModel)target == PlayerModel)
                    {
                        OnSaveLoaded();
                    }
                    break;
            }
        }
    }
}
