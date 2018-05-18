using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TutorialFPS.Services;
using TutorialFPS.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TutorialFPS.Controllers
{
    public class UIController : BaseController
    {
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _saveMenu;
        [SerializeField] private GameObject _loadMenu;

        private Dictionary<GameObject, bool> _hideableObjects;
        private Text _selectedSave;

        private void Start()
        {
            _hideableObjects = new Dictionary<GameObject, bool>();

            GameObject[] objs = GameObject.FindGameObjectsWithTag("HideInMenu");
            foreach (GameObject o in objs)
            {
                _hideableObjects.Add(o, true);
            }
        }

        public override void OnNotification(Notification notification, Object target, params object[] data)
        {
            base.OnNotification(notification, target, data);

            switch (notification)
            {
                case Notification.LoadMenu:
                    _mainMenu.SetActive(false);
                    _loadMenu.SetActive(true);
                    OnSaveLoadMenuEnable(_loadMenu);
                    break;

                case Notification.SaveMenu:
                    _mainMenu.SetActive(false);
                    _saveMenu.SetActive(true);
                    OnSaveLoadMenuEnable(_saveMenu);
                    break;

                case Notification.BackInMenu:
                    (target as GameObject).transform.parent.gameObject.SetActive(false);
                    _mainMenu.SetActive(true);
                    break;

                case Notification.PausePlay:
                    _mainMenu.SetActive(true);

                    foreach (GameObject hideableObject in _hideableObjects.Keys.ToList())
                    {
                        bool isActive = hideableObject.activeSelf;
                        _hideableObjects[hideableObject] = isActive;
                        if (isActive)
                        {
                            hideableObject.SetActive(false);
                        }
                    }
                    break;

                case Notification.ResumePlay:
                    ResumePlay();
                    break;

                case Notification.SaveLoad:
                    if (_selectedSave != null && !string.IsNullOrEmpty(_selectedSave.text))
                    {
                        if (_saveMenu!=null&& _saveMenu.activeSelf)
                        {
                            GameController.Instance.Save(Path.Combine(GameController.Instance.SavesDirectory, _selectedSave.text+".json"));
                            OnSaveLoadMenuEnable(_saveMenu);
                        }
                        if (_loadMenu != null && _loadMenu.activeSelf)
                        {
                            ResumePlay();
                            GameController.Instance.Load(Path.Combine(GameController.Instance.SavesDirectory, _selectedSave.text));
                        }
                    }

                    break;

                case Notification.Delete:
                    if (_selectedSave != null && !string.IsNullOrEmpty(_selectedSave.text))
                    {
                        GameController.Instance.DeleteSave(Path.Combine(GameController.Instance.SavesDirectory, _selectedSave.text));
                    }

                    OnSaveLoadMenuEnable(_loadMenu);
                    break;

                case Notification.SaveSelectionChanged:
                    if (_selectedSave != null)
                    {
                        _selectedSave.color = new Color(255f, 255f, 255f);
                    }

                    _selectedSave = target as Text;
                    _selectedSave.color = Color.red;
                    break;

                case Notification.NewGame:
                    ResumePlay();
                    break;
            }
        }

        private void OnSaveLoadMenuEnable(GameObject saveLoadMenu)
        {
            SavesScrollView savesScrollView = saveLoadMenu.GetComponentInChildren<SavesScrollView>();
            if (_selectedSave != null)
            {
                _selectedSave.color = new Color(255f, 255f, 255f);
                _selectedSave = null;
            }

            if (savesScrollView == null)
            {
                return;
            }

            string[] files = GameController.Instance.GetSaveFiles();
            savesScrollView.ContentUpdate(files);
        }

        private void ResumePlay()
        {
            if (_mainMenu != null)
            {
                _mainMenu.SetActive(false);
            }

            if (_saveMenu != null)
            {
                _saveMenu.SetActive(false);
            }

            if (_loadMenu != null)
            {
                _loadMenu.SetActive(false);
            }

            foreach (KeyValuePair<GameObject, bool> hideableObject in _hideableObjects.Where(x => x.Value))
            {
                hideableObject.Key.SetActive(true);
            }
        }
    }
}

