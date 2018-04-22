using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Models
{
    public class FlashlightModel : MonoBehaviour
    {
        private Light _light;

        public Light Light
        {
            get
            {
                if (_light != null)
                    return _light;

                return _light = GetComponent<Light>();
            }
        }

        private void Start()
        {
            Off();
        }

        public void On()
        {
            Light.enabled = true;
        }

        public void Off()
        {
            Light.enabled = false;

        }

        public void Switch()
        {
            Light.enabled = !Light.enabled;

        }
    }
}

