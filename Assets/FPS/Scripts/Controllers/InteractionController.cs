using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using TutorialFPS.Views;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialFPS.Controllers
{
    public class InteractionController : BaseController
    {
        private InteractionView _interactionView;

        [HideInInspector]
        public bool TextActive;
        public float Reach = 1.5F;

        private IInteractable _interactable;

        void Start()
        {
            _interactionView = FindObjectOfType<InteractionView>();
        }

        void FixedUpdate()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));

            RaycastHit hit; 

            if (Physics.Raycast(ray, out hit, Reach))
            {
                if (hit.collider.tag == "Interactable")
                {
                    if (TextActive == false && _interactable == null)
                    {
                        _interactable = hit.collider.gameObject.GetComponent<IInteractable>();

                        _interactionView.ShowMessage(_interactable.InteractionText);
                        TextActive = true;
                    }
                }

                else
                {
                    NothingDetected();
                }
            }

            else
            {
                NothingDetected();
            }
            
            Debug.DrawRay(ray.origin, ray.direction * Reach, Color.green);
        }

        private void NothingDetected()
        {
            _interactable = null;
            
            if (TextActive)
            {
                _interactionView.HideMessage();
                TextActive = false;
            }
        }

        public void Interact()
        {
            if (_interactable != null)
            {
                _interactable.Interact();
                _interactionView.ShowMessage(_interactable.InteractionText);
            }
        }
    }
}

