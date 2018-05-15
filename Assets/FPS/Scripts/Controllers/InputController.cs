using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Services;
using UnityEngine;

namespace TutorialFPS.Controllers
{
    /// <summary>
    /// Контроллер, который отвечает за горячие клавиши
    /// </summary>
    public sealed class InputController : BaseController
    {
        public bool InputEnabled = true;
        public bool PlayerInputEnabled = true;

        private void Update()
        {
            if (!InputEnabled)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameController.Instance.Notify(Notification.PausePlay,this);
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                GameController.Instance.QuickSave();
            }

            if (Input.GetKeyDown(KeyCode.F9))
            {
                GameController.Instance.LoadLastSave();
            }

            if (!PlayerInputEnabled)
            {
                return;
            }

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

            
        }
    }
}
