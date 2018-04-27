using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS.Models
{
    public abstract class PickableModel : BaseGameObject, IInteractable
    {
        private Transform _pickable;
        private Transform _defaultParent;

        protected bool _isPickedUp=false;

        public string InteractionText
        {
            get { return _isPickedUp? "release" : "pick up"; }
        }

        protected Transform Pickable
        {
            get
            {
                if ((object)_pickable==null)
                {
                    _pickable = Camera.main.transform.Find("Pickable");
                }

                return _pickable; 
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _defaultParent = Transform.parent;
        }

        public void Interact()
        {
            Transform.parent = _isPickedUp? _defaultParent : Pickable;
            Rigidbody.isKinematic = !_isPickedUp;
            _isPickedUp = !_isPickedUp;
        }
    }
}

