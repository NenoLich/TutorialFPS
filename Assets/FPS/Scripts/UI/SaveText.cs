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
            
            if (_lastClickTime + maxDoubleClickTimeout < Time.realtimeSinceStartup)
            {
                GameController.Instance.Notify(Notification.Load, gameObject);
            }
            _lastClickTime = Time.realtimeSinceStartup;
        }
    }
}

