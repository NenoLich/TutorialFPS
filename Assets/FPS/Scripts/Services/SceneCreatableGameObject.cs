using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Services
{
    [Serializable]
    public class SceneCreatableGameObject
    {
        public string Name;
        public GameObject Prefab;
        public int Count;
    }
}

