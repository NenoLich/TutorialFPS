using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Models;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialFPS.Views
{
    public class FlashlightBatteryView : MonoBehaviour
    {
        private Slider _battery;
        private FlashlightModel _flashlightModel;
        private bool _lowCharge;
        private Image _background;

        private void Awake()
        {
            _flashlightModel= FindObjectOfType<FlashlightModel>();
            _battery = GetComponent<Slider>();
            _lowCharge = false;
            _background = transform.Find("Background").GetComponent<Image>();
        }

        private void Update()
        {
            if (Mathf.Abs(_battery.value - _flashlightModel.batteryCharge)>1)
            {
                _battery.value = _flashlightModel.batteryCharge;
                if (_battery.value < 10 && !_lowCharge)
                {
                    StartCoroutine(Warning());
                    _lowCharge = true;
                }
            }
        }

        private IEnumerator Warning()
        {
            while (_battery.value < 10)
            {
                _background.color=Color.red;
                yield return new WaitForSeconds(0.5f);

                _background.color = Color.white;
                yield return new WaitForSeconds(0.5f);
            }

            _lowCharge = false;
        }
    }
}

