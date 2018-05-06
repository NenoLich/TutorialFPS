using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Models;
using UnityEngine;

namespace TutorialFPS.Controllers
{
    public sealed class FlashlightController : BaseController
    {
        private FlashlightModel _flashlightModel;

        public FlashlightModel FlashlightModel
        {
            get
            {
                if (_flashlightModel == null)
                    _flashlightModel=FindObjectOfType<FlashlightModel>();

                return _flashlightModel;
            }
        }

        public void On()
        {
            FlashlightModel.On();
        }

        public void Off()
        {
            FlashlightModel.Off();
        }

        public void Switch()
        {
            FlashlightModel.Switch();
        }
    }
}
