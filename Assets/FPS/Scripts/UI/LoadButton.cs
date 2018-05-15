using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Controllers;
using TutorialFPS.Services;
using UnityEngine;

namespace TutorialFPS.UI
{
    public class LoadButton : MonoBehaviour
    {
        public void LoadButtonClick()
        {
            GameController.Instance.Notify(Notification.Load, gameObject);
        }
    }
}
