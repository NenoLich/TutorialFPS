using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Controllers;
using TutorialFPS.Services;
using UnityEngine;

namespace TutorialFPS.UI
{
    public class SaveButton : MonoBehaviour
    {
        public void SaveButtonClick()
        {
            GameController.Instance.Notify(Notification.SaveLoad, gameObject);
        }
    }
}
