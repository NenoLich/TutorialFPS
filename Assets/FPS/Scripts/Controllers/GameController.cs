using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TutorialFPS.Controllers
{
    public class GameController : BaseController
    {
        private AsyncOperation _async;
        private bool _loadingFinished = false;
        private bool _activationRequested = false;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            StartCoroutine(LoadScene("StartLevel"));
        }

        public void ActivateScene(string path="")
        {
            _activationRequested = true;
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

        private void Update()
        {
            if (_async != null &&_loadingFinished &&_activationRequested)
            {
                _async.allowSceneActivation = true;
            }
        }
    }
}
