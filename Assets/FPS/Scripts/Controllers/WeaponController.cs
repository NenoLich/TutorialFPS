using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Controllers
{
    public class WeaponController : BaseController
    {
        private int _currentWeaponId;
        private int _currentAmmoId;

        private void Start()
        {
            for (int i = 1; i < Main.Instance.ObjectManager.Weapons.Length; i++)
                Main.Instance.ObjectManager.Weapons[i].IsVisible = false;
        }

        public void ChangeWeapon()
        {
            Main.Instance.ObjectManager.Weapons[_currentWeaponId].IsVisible = false;

            _currentWeaponId++;
            if (_currentWeaponId >= Main.Instance.ObjectManager.Weapons.Length)
                _currentWeaponId = 0;

            Main.Instance.ObjectManager.Weapons[_currentWeaponId].IsVisible = true;
        }

        public void Fire()
        {
            Main.Instance.ObjectManager.Weapons[_currentWeaponId].Fire();
        }
    }
}
