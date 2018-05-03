using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Models.AI
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(AIModel aiModel);
    }
}