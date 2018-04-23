using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Controllers
{
    public abstract class BaseController : MonoBehaviour
    {
        public bool isEnabled { get; private set; }

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


