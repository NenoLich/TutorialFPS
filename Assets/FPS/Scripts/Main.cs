using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Controllers;
using TutorialFPS.Services;
using UnityEngine;

namespace TutorialFPS
{
    [DefaultExecutionOrder(-2)]
    public class Main : MonoBehaviour
    {
        private ObjectManager _objectManager;
        private ObjectPool _objectPool;

        public static Main Instance { get; private set; }
        public static GameObject Player { get; private set; }
        public InputController InputController { get; private set; }
        public FlashlightController FlashlightController { get; private set; }
        public InteractionController InteractionController { get; private set; }
        public WeaponController WeaponController { get; private set; }

        public ObjectManager ObjectManager
        {
            get
            {
                if ((object)_objectManager==null)
                {
                    _objectManager= GetComponent<ObjectManager>();
                }

                return _objectManager;
            }
        }

        public ObjectPool ObjectPool
        {
            get
            {
                if ((object)_objectPool == null)
                {
                    _objectPool = GetComponent<ObjectPool>();
                }

                return _objectPool;
            }
        }

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
            InteractionController = gameObject.AddComponent<InteractionController>();
            WeaponController = gameObject.AddComponent<WeaponController>();
        }
    }
}