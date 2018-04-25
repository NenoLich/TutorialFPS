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
                if ((object)_flashlightModel == null)
                    _flashlightModel=FindObjectOfType<FlashlightModel>();

                return _flashlightModel;
            }
        }

        public override void On()
        {
            if (isEnabled) return;

            base.On();
            FlashlightModel.On();
        }

        public override void Off()
        {
            if (!isEnabled) return; 

            base.Off();
            FlashlightModel.Off();
        }

        public void Switch()
        {
            FlashlightModel.Switch();
        }
    }
}
