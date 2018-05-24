using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

namespace TutorialFPS.Services.Network
{
    [DefaultExecutionOrder(-1)]
    public class PlayerSetUp : NetworkBehaviour
    {
        public MeshRenderer _renderer;

        void Start()
        {
            if (isLocalPlayer)
            {
                Camera _camera = GetComponentInChildren<Camera>();
                _camera.enabled = true;
                _camera.gameObject.tag = "MainCamera";
                _camera.GetComponent<AudioListener>().enabled = true;
                _camera.GetComponent<HeadBob>().enabled = true;
                transform.name = "Player " + GetComponent<NetworkIdentity>().netId;

                GetComponent<RigidbodyFirstPersonController>().enabled = true;
                //Main.Instance.RegisterPlayer(gameObject);
            }
            else
            {
                _renderer.enabled = true;
            }
        }
    }
}

