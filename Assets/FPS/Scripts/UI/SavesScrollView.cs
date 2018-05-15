using System.Collections;
using System.Collections.Generic;
using System.IO;
using TutorialFPS.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialFPS.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class SavesScrollView : MonoBehaviour
    {
        [SerializeField] private Text _savePrefab;
        [SerializeField] private float __verticalOffset=3f;
        private RectTransform _content;

        public RectTransform Content
        {
            get
            {
                if (_content == null)
                {
                    _content = GetComponent<ScrollRect>().content;
                }

                return _content;
            }
        }

        public void ContentUpdate(string[] files)
        {
            foreach (RectTransform rectTransform in Content)
            {
                DestroyImmediate(rectTransform.gameObject);
            }

            float posY = 0f;
            foreach (string file in files)
            {
                Text save = Instantiate(_savePrefab);
                save.text = file;
                RectTransform prefabTransform = save.rectTransform;
                prefabTransform.SetParent(Content, false);
                prefabTransform.position = new Vector3(
                    prefabTransform.position.x, prefabTransform.position.y+ (posY-= prefabTransform.rect.height- __verticalOffset), prefabTransform.position.z);
            }

            posY -= posY / files.Length / 2;
            Content.sizeDelta=new Vector2(Content.sizeDelta.x, Mathf.Max(Content.sizeDelta.y, -posY));
        }
    }
}
