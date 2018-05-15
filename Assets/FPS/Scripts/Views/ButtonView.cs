using System;
using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TutorialFPS.Views
{
    [RequireComponent(typeof(Outline))]
    public class ButtonView : MonoBehaviour
    {
        [SerializeField] private float _animationSpeed = 5f;
        [SerializeField] private float _maxXEffectDistance = 30f;
        [SerializeField] private UnityEvent OnClick;

        private Outline _outline;
        private Image image;

        private void Start()
        {
            _outline = GetComponent<Outline>();
            image = GetComponent<Image>();
        }

        public void OnPointerEnter(BaseEventData eventData)
        {
            if (_outline.enabled)
            {
                return;
            }

            _outline.enabled = true;
        }

        public void OnPointerExit(BaseEventData eventData)
        {
            if (!_outline.enabled|| !image.raycastTarget)
            {
                return;
            }

            _outline.enabled = false;
        }

        public void OnPointerClick(BaseEventData eventData)
        {
            SetRaycastTarget(false);

            if (!_outline.enabled)
            {
                _outline.enabled = true;
            }

            StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            while (_outline.effectDistance.x < _maxXEffectDistance)
            {
                _outline.effectDistance = new Vector2(Mathf.SmoothStep(_outline.effectDistance.x, _maxXEffectDistance,
                    _animationSpeed * 0.05f) +0.3f, _outline.effectDistance.y);

                yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.05f));
            }

            SetRaycastTarget(true);
            OnClick.Invoke();
            _outline.effectDistance=new Vector2(2, _outline.effectDistance.y);
        }

        private void SetRaycastTarget(bool flag)
        {
            foreach (Image image in transform.root.GetComponentsInChildren<Image>())
            {
                image.raycastTarget = flag;
            }
        }
    }
    
}

