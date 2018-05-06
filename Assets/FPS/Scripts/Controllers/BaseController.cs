using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Services;
using UnityEngine;

namespace TutorialFPS.Controllers
{
    public abstract class BaseController : MonoBehaviour
    {
        public virtual void OnNotification(Notification notification, Object target, params object[] data)
        {

        }

    }
}


