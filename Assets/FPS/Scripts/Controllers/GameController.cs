using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TutorialFPS.Interfaces;
using TutorialFPS.Services;
using TutorialFPS.Services.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TutorialFPS.Controllers
{
    [DefaultExecutionOrder(-1)]
    public class GameController : BaseController
    {
        public string SavesDirectory;

        private DataManager _dataManager = new DataManager();
        private string _password = "vwen21cjnj73hnncew";
        private ISavable[] _iSavables;
        private AsyncOperation _async;
        private bool _loadingFinished = false;
        private bool _activationRequested = false;
        private bool _defaultSaveComplete = false;
        private bool _firstLoad = true;
        private InputController _inputController;
        private PlayerController _playerController;
        private BaseController[] _baseControllers;

        public static GameController Instance { get; private set; }

        private ISavable[] ISavables
        {
            get
            {
                if (_iSavables == null)
                {
                    _iSavables = FindObjectsOfType<GameObject>().
                        Select(x => x.GetComponent<ISavable>()).
                        Where(y => y != null).ToArray();
                }

                return _iSavables;
            }

            set { _iSavables = value; }
        }

        public BaseController[] Controllers
        {
            get
            {
                _baseControllers = GetAllControllers();
                return _baseControllers;
            }

            set { _baseControllers = value; }
        }

        private InputController InputController
        {
            get
            {
                if (_inputController == null && Main.Instance != null)
                {
                    _inputController = Main.Instance.InputController;
                }

                return _inputController;
            }
        }

        private PlayerController PlayerController
        {
            get
            {
                if (_playerController == null && Main.Instance != null)
                {
                    _playerController = Main.Instance.PlayerController;
                }

                return _playerController;
            }
        }

        private void Awake()
        {
            if (Instance)
                DestroyImmediate(this);
            else
                Instance = this;

            DontDestroyOnLoad(gameObject);
            _dataManager.SetData<JSonSerializer>();
            SavesDirectory = Path.Combine(Application.dataPath, "Saves");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StartCoroutine(OnSceneWasLoaded(scene));
        }

        private IEnumerator OnSceneWasLoaded(Scene scene)
        {
            yield return new WaitForEndOfFrame();

            Controllers = GetAllControllers();

            ISavables = FindObjectsOfType<GameObject>().
                Select(x => x.GetComponent<ISavable>()).
                Where(y => y != null).ToArray();

            Save(Path.Combine(Application.dataPath, "DefaultSave" + scene.name + ".json"));
        }

        private IEnumerator LoadScene(string sceneName)
        {
            if (sceneName == "")
                yield break;

            _async = SceneManager.LoadSceneAsync(sceneName);
            _async.allowSceneActivation = false;

            while (_async.progress < 0.9f)
            {
                yield return null;
            }

            _loadingFinished = true;
        }

        public void Notify(Notification notification, Object target, params object[] data)
        {
            foreach (BaseController c in Controllers)
            {
                c.OnNotification(notification, target, data);
            }
        }

        public BaseController[] GetAllControllers()
        {
            return FindObjectsOfType<BaseController>();
        }

        private void Update()
        {
            if (_firstLoad)
            {
                StartCoroutine(LoadScene("StartLevel"));
                _firstLoad = false;
            }

            if (_async != null && _loadingFinished && _activationRequested)
            {
                _async.allowSceneActivation = true;
                _loadingFinished = false;
                _activationRequested = false;
                _defaultSaveComplete = true;
            }
        }

        public void Save(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            Data[] data = ISavables.Select(x => x.Data).ToArray();
            _dataManager.Save(data, path, _password);
        }

        public void QuickSave()
        {
            Save(Path.Combine(SavesDirectory, "QuickSave"));
        }

        public void Load(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            Data[] data = _dataManager.Load(path, _password);
            foreach (Data dataItem in data)
            {
                ISavable savable = ISavables.First(x => x.Data.Name == dataItem.Name);
                if (savable != null)
                {
                    savable.Data = dataItem;
                }
            }

            ResumePlay();
        }

        public void LoadDefault()
        {
            if (_defaultSaveComplete)
            {
                Load(Path.Combine(Application.dataPath, "DefaultSave" + SceneManager.GetActiveScene().name + ".json"));
            }
            else
            {
                _activationRequested = true;
            }

            ResumePlay();
        }

        public void LoadLastSave()
        {
            Load(Path.Combine(SavesDirectory, GetSaveFiles().First()));
        }

        public void DeleteSave(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            File.Delete(path);
        }

        public string[] GetSaveFiles()
        {
            if (!Directory.Exists(SavesDirectory))
            {
                Directory.CreateDirectory(SavesDirectory);
            }

            DirectoryInfo info = new DirectoryInfo(SavesDirectory);
            string[] files = info.GetFiles().OrderByDescending(p => p.CreationTime).Select(x => x.Name).ToArray();
            return files;
        }

        public override void OnNotification(Notification notification, Object target, params object[] data)
        {
            base.OnNotification(notification, target, data);

            switch (notification)
            {
                case Notification.PausePlay:
                    PausePlay();
                    break;

                case Notification.ResumePlay:
                    ResumePlay();
                    break;
            }
        }

        private void PausePlay()
        {
            Time.timeScale = 0f;
            InputController.InputEnabled = false;
            PlayerController.PlayerModel.SetCursorLock(false);
        }

        private void ResumePlay()
        {
            if (InputController != null)
            {
                InputController.InputEnabled = true;
            }

            if (PlayerController != null)
            {
                PlayerController.PlayerModel.SetCursorLock(true);
            }

            Time.timeScale = 1f;
        }
    }
}
