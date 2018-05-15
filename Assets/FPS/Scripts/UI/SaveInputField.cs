using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Controllers;
using TutorialFPS.Services;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialFPS.UI
{
    public class SaveInputField : MonoBehaviour
    {
        [SerializeField] private Text inputText;

        public void SaveInputFieldInteraction(string value)
        {
            GameController.Instance.Notify(Notification.SaveSelectionChanged, inputText);
        }
    }
}

