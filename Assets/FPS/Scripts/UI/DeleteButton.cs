using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Controllers;
using TutorialFPS.Services;
using UnityEngine;

namespace TutorialFPS.UI
{
    public class DeleteButton : MonoBehaviour
    {
        public void DeleteButtonClick()
        {
            GameController.Instance.Notify(Notification.Delete, gameObject);
        }
    }
}
