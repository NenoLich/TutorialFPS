using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Models.AI
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(AIModel aiModel);
    }
}
