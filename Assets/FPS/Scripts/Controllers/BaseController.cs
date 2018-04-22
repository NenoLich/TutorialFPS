using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Controllers
{
    public abstract class BaseController : MonoBehaviour
    {
        public bool isEnabled { get; private set; }

        private Camera _camera;

        protected Camera Camera
        {
            get
            {
                if (_camera != null)
                    return _camera;

                return _camera = Camera.main;
            }
        }

        public virtual void On()
        {
            isEnabled = true;
        }

        public virtual void Off()
        {
            isEnabled = false;
        }
    }
}


