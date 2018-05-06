using System;
using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Models;
using TutorialFPS.Views;
using UnityEngine;

namespace TutorialFPS.Services
{
    [Serializable]
    public class Weapon
    {
        public WeaponModel weaponModel;
        public WeaponView weaponView;

        public bool IsVisible
        {
            get { return weaponModel.IsVisible && weaponView.gameObject.activeSelf; }
            set
            {
                weaponModel.IsVisible = value;

                if (weaponView != null)
                {
                    weaponView.gameObject.SetActive(value);
                }
            }
        }
    }
}
