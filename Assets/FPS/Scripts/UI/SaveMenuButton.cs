using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Controllers;
using TutorialFPS.Services;
using UnityEngine;

namespace TutorialFPS.UI
{
    public class SaveMenuButton : MonoBehaviour
    {
        public void LoadMenuClick()
        {
            GameController.Instance.Notify(Notification.SaveMenu, gameObject);
        }
    }
}
