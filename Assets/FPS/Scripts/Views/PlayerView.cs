using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialFPS.Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private Image _hitImage;
        [SerializeField]
        private Text _hitpoints;

        public void UpdateHealth(int hitpoints)
        {
            if (!string.IsNullOrEmpty(_hitpoints.text))
            {
                StartCoroutine(ChangeImageAlpha());
            }

            _hitpoints.text = hitpoints.ToString();
        }

        private IEnumerator ChangeImageAlpha()
        {
            while (_hitImage.color.a != 1f)
            {
                _hitImage.color = Color.Lerp(_hitImage.color, new Color(_hitImage.color.r, _hitImage.color.g, _hitImage.color.b, 1f),Time.deltaTime* 200f);
                yield return new WaitForSeconds(0.1f);
            }
            while (_hitImage.color.a != 0f)
            {
                _hitImage.color = Color.Lerp(_hitImage.color, new Color(_hitImage.color.r, _hitImage.color.g, _hitImage.color.b, 0f), Time.deltaTime * 200f);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

