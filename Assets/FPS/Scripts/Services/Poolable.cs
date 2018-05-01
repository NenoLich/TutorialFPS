using System;
using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS.Services
{
    [Serializable]
    public class Poolable
    {
        public GameObject gameObject;
        public int quantity;
    }
}

