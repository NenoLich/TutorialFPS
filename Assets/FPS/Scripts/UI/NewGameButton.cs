using System.Collections;
using System.Collections.Generic;
using System.IO;
using TutorialFPS.Controllers;
using UnityEngine;

namespace TutorialFPS.UI
{
    public class NewGameButton : MonoBehaviour
    {
        public void NewGame()
        {
            GameController.Instance.LoadDefault();
        }
    }
}
