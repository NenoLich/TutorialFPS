using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Controllers
{
    /// <summary>
    /// Контроллер, который отвечает за горячие клавиши
    /// </summary>
    public sealed class InputController : BaseController
    {
        private void Update()
        {
            if (Input.GetButtonDown("Flashlight"))
            {
                Main.Instance.FlashlightController.Switch();
            }

            if (Input.GetButtonDown("Interact"))
            {
                Main.Instance.InteractionController.Interact();
            }
        }
    }
}
