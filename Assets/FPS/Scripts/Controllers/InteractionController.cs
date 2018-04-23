using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialFPS.Controllers
{
    public class InteractionController : BaseController
    {
        private GameObject TextPrefab;

        [HideInInspector]
        public bool TextActive;

        public float Reach = 1.5F;

        private GameObject textPrefabInstance;
        private GameObject interactable;

        void Start()
        {
            TextPrefab = Resources.Load("TextPrefab") as GameObject;

            if (TextPrefab == null)
                Debug.Log("<color=yellow><b>No TextPrefab was found.</b></color>");
        }

        void FixedUpdate()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));

            RaycastHit hit; 

            if (Physics.Raycast(ray, out hit, Reach))
            {
                if (hit.collider.tag == "Interactable")
                {
                    interactable = hit.collider.gameObject;

                    if (TextActive == false && TextPrefab != null)
                    {
                        textPrefabInstance = Instantiate(TextPrefab);
                        TextActive = true;
                        textPrefabInstance.transform.SetParent(Main.Player.transform, true); 
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
            interactable = null;

            if (TextActive)
            {
                DestroyImmediate(textPrefabInstance);
                TextActive = false;
            }
        }

        public void Interact()
        {
            if (interactable != null)
            {
                interactable.GetComponent<IInteractable>().Interact();
            }
        }
    }
}

