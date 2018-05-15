using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Controllers;
using TutorialFPS.Services;
using UnityEngine;

namespace TutorialFPS.UI
{
    public class ResumeButton : MonoBehaviour
    {
        public void ResumeButtonClick()
        {
            GameController.Instance.Notify(Notification.ResumePlay, gameObject);
        }
    }
}
