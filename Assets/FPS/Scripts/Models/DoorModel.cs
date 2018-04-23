using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Models
{
    public class DoorModel : BaseGameObject,IInteractable
    {
        public float moveForce = 650f;

        private Vector3 _forward;
        private Vector3 _defaultRotation;

        public Vector3 Forward
        {
            get
            {
                if (_forward != Vector3.zero)
                    return _forward;

                return _forward = transform.Find("Forward").position - Position;
            }
        }
        private void Start()
        {
            _defaultRotation = Rotation.eulerAngles;
        }

        public void Interact()
        {
            Rigidbody.constraints = RigidbodyConstraints.None;
            Rigidbody.AddForce(new Vector3(Forward.x != 0f ? (Position.x < Camera.main.transform.position.x ? -1 : 1) : 0,
                                   Forward.y != 0f ? (Position.y < Camera.main.transform.position.y ? -1 : 1) : 0,
                                   Forward.z != 0f ? (Position.z < Camera.main.transform.position.z ? -1 : 1) : 0) * moveForce);

            StartCoroutine(Closing());
        }

        private IEnumerator Closing()
        {
            yield return new WaitForFixedUpdate();
            bool condition = (Rotation.eulerAngles.y>270f? Rotation.eulerAngles.y-360f: Rotation.eulerAngles.y) < _defaultRotation.y;
            while (condition == (Rotation.eulerAngles.y > 270f ? Rotation.eulerAngles.y - 360f : Rotation.eulerAngles.y) < _defaultRotation.y)
                yield return new WaitForFixedUpdate();

            Rotation = Quaternion.Euler(_defaultRotation);
            Rigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        }
    }
}