using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TutorialFPS.UI
{
    public class ExitButton : MonoBehaviour
    {
        public void Exit()
        {
            Application.Quit();
        }
    }
}

