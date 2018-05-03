using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TutorialFPS.Models.AI
{
    [Serializable]
    public class Transition
    {
        public Decision decision;
        public AIBehaviour trueBehaviour;
        public AIBehaviour falseBehaviour;
    }
}