using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Controllers;
using UnityEngine;

namespace TutorialFPS
{
    [DefaultExecutionOrder(-1)]
    public class Main : MonoBehaviour
    {
        public static Main Instance { get; private set; }
        public static GameObject Player { get; private set; }
        public InputController InputController { get; private set; }
        public FlashlightController FlashlightController { get; private set; }
        public DoorController DoorController { get; private set; }

        private void Awake()
        {
            if (Instance)
                DestroyImmediate(this);
            else
                Instance = this;

            Player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Start()
        {
            InputController = gameObject.AddComponent<InputController>();
            FlashlightController = gameObject.AddComponent<FlashlightController>();
            DoorController = gameObject.AddComponent<DoorController>();
        }
    }
}