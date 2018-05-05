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

            if (Input.GetButton("Fire1"))
            {
                Main.Instance.WeaponController.Fire();

            }

            if (Input.GetButton("Fire2"))
            {
                Main.Instance.WeaponController.AlternateFire();

            }

            if (Input.GetButtonDown("SwitchWeapon"))
            {
                Main.Instance.WeaponController.SwitchWeapon();

            }

            float deltaScroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(deltaScroll) > 0.075f)
            {
                Main.Instance.WeaponController.ChangeWeapon(deltaScroll);
            }

            if (Input.GetButtonDown("Reload"))
            {
                Main.Instance.WeaponController.Reload();
            }

            //if (Input.GetButtonDown("MoveAgent"))
            //{
            //    Main.Instance.NavMeshController.SetDestinationPoint();
            //}
        }
    }
}
