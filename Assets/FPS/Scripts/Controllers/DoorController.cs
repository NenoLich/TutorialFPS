using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TutorialFPS.Models;
using UnityEngine;

namespace TutorialFPS.Controllers
{
    public class DoorController : BaseController
    {
        public GameObject TextPrefab;
        public GameObject CrosshairPrefab;

        [HideInInspector]
        public bool TextActive;

        // Raycast Settings
        public float Reach = 1.5F;

        public string Character = "e";

        private GameObject crosshairPrefabInstance;
        private GameObject textPrefabInstance;
        private GameObject interactable;

        void Start()
        {
            TextPrefab = Resources.Load("TextPrefab") as GameObject;
            CrosshairPrefab = Resources.Load("CrosshairPrefab") as GameObject;

            if (CrosshairPrefab == null)
                Debug.Log("<color=yellow><b>No CrosshairPrefab was found.</b></color>"); // Return an error if no crosshair was specified

            else
            {
                crosshairPrefabInstance = Instantiate(CrosshairPrefab); // Display the crosshair prefab
                crosshairPrefabInstance.transform.SetParent(Main.Player.transform, true); // Make the player the parent object of the crosshair prefab
            }

            if (TextPrefab == null)
                Debug.Log("<color=yellow><b>No TextPrefab was found.</b></color>"); // Return an error if no text element was specified
        }

        void Update()
        {
            // Set origin of ray to 'center of screen' and direction of ray to 'cameraview'
            Ray ray = Camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));

            RaycastHit hit; // Variable reading information about the collider hit

            // Cast ray from center of the screen towards where the player is looking
            if (Physics.Raycast(ray, out hit, Reach))
            {
                if (hit.collider.tag == "Door")
                {
                    interactable = hit.collider.gameObject;

                    // Display the UI element when the player is in reach of the door
                    if (TextActive == false && TextPrefab != null)
                    {
                        textPrefabInstance = Instantiate(TextPrefab);
                        TextActive = true;
                        textPrefabInstance.transform.SetParent(Main.Player.transform, true); // Make the player the parent object of the text element
                    }
                }

                else
                {
                    interactable = null;

                    // Destroy the UI element when Player is no longer in reach of the door
                    if (TextActive)
                    {
                        DestroyImmediate(textPrefabInstance);
                        TextActive = false;
                    }
                }
            }

            else
            {
                interactable=null;

                // Destroy the UI element when Player is no longer in reach of the door
                if (TextActive)
                {
                    DestroyImmediate(textPrefabInstance);
                    TextActive = false;
                }
            }

            //Draw the ray as a colored line for debugging purposes.
            Debug.DrawRay(ray.origin, ray.direction * Reach, Color.green);
        }

        public void Open()
        {
            if (interactable!=null)
            {
                interactable.GetComponent<DoorModel>().Open();
            }
        }
    }
}
