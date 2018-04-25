﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialFPS.Views
{
    public class InteractionView : MonoBehaviour
    {
        private Text _interactionText;
        private string _defaultText;

        public Text InteractionText
        {
            get
            {
                if ((object)_interactionText==null)
                {
                    _interactionText = GetComponent<Text>();
                    _defaultText = _interactionText.text;
                }

                return _interactionText; 
            }
        }

        public void ShowMessage(string interactionText)
        {
            if (!InteractionText.enabled)
            {
                InteractionText.text += interactionText;
                InteractionText.enabled = true;
            }
        }

        public void HideMessage()
        {
            if (_interactionText.enabled)
            {
                InteractionText.text = _defaultText;
                InteractionText.enabled = false;
            }
        }
    }
}
