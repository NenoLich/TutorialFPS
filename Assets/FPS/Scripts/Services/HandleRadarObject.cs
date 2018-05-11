using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Views;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialFPS.Services
{
    public class HandleRadarObject : MonoBehaviour
    {
        public Image Image;

        private void Start()
        {
            RadarView.RegisterRadarObject(gameObject, Image);
            GetComponent<BaseGameObject>().VisibilityChanged += OnVisibilityChanged;
        }
        private void OnDestroy()
        {
            RadarView.RemoveRadarObject(gameObject);
        }

        private void OnVisibilityChanged(bool isVisible)
        {
            if (isVisible)
            {
                RadarView.RegisterRadarObject(gameObject, Image);
                return;
            }

            RadarView.RemoveRadarObject(gameObject);
        }
    }
}

