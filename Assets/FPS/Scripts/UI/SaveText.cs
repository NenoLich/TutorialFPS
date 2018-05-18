using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Controllers;
using TutorialFPS.Services;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialFPS.UI
{
    public class SaveText : MonoBehaviour
    {
        [SerializeField] private float maxDoubleClickTimeout = 0.3f;
        private float _lastClickTime;

        public void SaveTextClick()
        {
            GameController.Instance.Notify(Notification.SaveSelectionChanged, GetComponent<Text>());
            
            if (Time.realtimeSinceStartup - maxDoubleClickTimeout < _lastClickTime)
            {
                GameController.Instance.Notify(Notification.SaveLoad, gameObject);
            }
            _lastClickTime = Time.realtimeSinceStartup;
        }
    }
}

