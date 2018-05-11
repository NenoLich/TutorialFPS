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
        private AIController _aiController;
        private GameController _gameController;

        public static Main Instance { get; private set; }
        public static GameObject Player { get; private set; }
        public InputController InputController { get; private set; }
        public FlashlightController FlashlightController { get; private set; }
        public InteractionController InteractionController { get; private set; }
        public WeaponController WeaponController { get; private set; }
        public PlayerController PlayerController { get; private set; }

        #region Properties

        public ObjectManager ObjectManager
        {
            get
            {
                if (_objectManager == null)
                {
                    _objectManager = GetComponent<ObjectManager>();
                }

                return _objectManager;
            }
        }

        public ObjectPool ObjectPool
        {
            get
            {
                if (_objectPool == null)
                {
                    _objectPool = GetComponent<ObjectPool>();
                }

                return _objectPool;
            }
        }

        public AIController AiController
        {
            get
            {
                if (_aiController == null)
                {
                    _aiController = GetComponent<AIController>();
                }

                return _aiController;
            }
        }

        public GameController GameController
        {
            get
            {
                if (_gameController == null)
                {
                    _gameController = GetComponent<GameController>();
                }

                return _gameController;
            }
        }

        #endregion

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
            PlayerController = gameObject.AddComponent<PlayerController>();
        }

        public void Notify(Notification notification, Object target, params object[] data)
        {
            BaseController[] controller_list = GetAllControllers();
            foreach (BaseController c in controller_list)
            {
                c.OnNotification(notification, target, data);
            }
        }

        public BaseController[] GetAllControllers()
        {
            return GetComponents<BaseController>();
        }
    }
}