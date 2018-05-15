using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Controllers;
using TutorialFPS.Services;
using UnityEngine;

namespace TutorialFPS.UI
{
    public class LoadMenuButton : MonoBehaviour
    {
        public void LoadMenuClick()
        {
            GameController.Instance.Notify(Notification.LoadMenu, gameObject);
        }
    }
}
