using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Models
{
    public class FlashlightModel : BaseGameObject
    {
        public float batteryCharge;

        [SerializeField]
        private float maxBatteryCharge = 100;
        [SerializeField]
        private float workingTime = 100;
        private Light _light;
        private float _baseIntensity;
        private bool charging;

        protected override void Awake()
        {
            base.Awake();
            _light = GetComponent<Light>();
            batteryCharge = maxBatteryCharge;
            _baseIntensity = _light.intensity;
            charging = false;
        }

        private void Start()
        {
            Off();
        }

        private void Update()
        {
            if (!charging)
            {
                charging = true;
                StartCoroutine(_light.enabled ? SpendingEnergy() : AccumulateEnergy());
            }

            if (batteryCharge < maxBatteryCharge * 0.1f)
            {
                _light.intensity = _baseIntensity / 2;
            }
            else
            {
                _light.intensity = _baseIntensity;
            }

            if (batteryCharge < 2f)
            {
                _light.enabled = false;
            }
        }

        private IEnumerator SpendingEnergy()
        {
            while (_light.enabled && batteryCharge > 0)
            {
                yield return new WaitForSeconds(workingTime / maxBatteryCharge);
                batteryCharge--;
            }

            charging = false;
        }

        private IEnumerator AccumulateEnergy()
        {
            while (!_light.enabled && batteryCharge < maxBatteryCharge)
            {
                yield return new WaitForSeconds(0.5f);
                batteryCharge++;
            }

            charging = false;
        }

        public void On()
        {
            _light.enabled = true;
        }

        public void Off()
        {
            _light.enabled = false;

        }

        public void Switch()
        {
            _light.enabled = !_light.enabled;

        }
    }
}

