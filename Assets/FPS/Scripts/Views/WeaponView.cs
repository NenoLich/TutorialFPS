﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialFPS.Views
{
    public class WeaponView : MonoBehaviour
    {
        private Image _ammoFillArea;

        private Image AmmoFillArea
        {
            get
            {
                if ((object)_ammoFillArea==null)
                {
                    _ammoFillArea = transform.Find("AmmoFillArea").GetComponent<Image>();
                }

                return _ammoFillArea; 
            }
        }

        public void SetMagazineView(int magazine, int maxMagazine)
        {
            AmmoFillArea.fillAmount = (float)magazine / maxMagazine;
        }
    }
}
