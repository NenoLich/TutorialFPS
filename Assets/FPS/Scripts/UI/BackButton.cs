using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Controllers;
using TutorialFPS.Services;
using UnityEngine;

namespace TutorialFPS.UI
{
    public class BackButton : MonoBehaviour
    {
        public void BackButtonClick()
        {
            GameController.Instance.Notify(Notification.BackInMenu, gameObject);
        }
    }
}
