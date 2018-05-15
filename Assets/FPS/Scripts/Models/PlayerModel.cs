﻿using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Controllers;
using TutorialFPS.Interfaces;
using TutorialFPS.Services;
using TutorialFPS.Services.Data;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace TutorialFPS.Models
{
    public class PlayerModel : BaseGameObject, IDamagable, ISavable
    {
        [HideInInspector] public int CurrentWeaponId;
        [HideInInspector] public int[] WeaponsMagazine;

        [SerializeField]
        private float _maxHealth = 100f;

        private float _health;
        private RigidbodyFirstPersonController _firstPersonController;

        public Data Data
        {
            get
            {
                return new Data
                {
                    Name = Name,
                    Position = Position,
                    Rotation = Rotation,
                    Scale = Scale,
                    HitPoints = Health,
                    CurrentWeaponID = CurrentWeaponId,
                    WeaponsMagazine = WeaponsMagazine,
                    IsVisible = IsVisible
                };
            }
            set
            {
                Position = value.Position;
                Rotation = value.Rotation;
                Scale = value.Scale;
                Health = value.HitPoints;
                CurrentWeaponId = value.CurrentWeaponID;
                WeaponsMagazine = value.WeaponsMagazine;
                IsVisible = value.IsVisible;

                OnSaveLoaded();
                GameController.Instance.Notify(Notification.SaveLoaded, this);
            }
        }

        public float Health
        {
            get { return _health; }
            private set
            {
                _health = value;
                GameController.Instance.Notify(Notification.UpdateHealth, this);
            }
        }

        protected void Start()
        {
            Health = _maxHealth;
            _firstPersonController = GetComponent<RigidbodyFirstPersonController>();
        }

        private void OnSaveLoaded()
        {
            _firstPersonController.mouseLook = new UnityStandardAssets.Characters.FirstPerson.MouseLook();
            _firstPersonController.mouseLook.Init(Transform, Camera.main.transform);
        }

        protected override void SetVisibility(Transform objTransform, bool visible)
        {
            _firstPersonController.enabled = visible;
            Main.Instance.InputController.PlayerInputEnabled = visible;
            Rigidbody.constraints = visible?RigidbodyConstraints.FreezeRotation :RigidbodyConstraints.None;

        }

        public void Death()
        {
            SetVisibility(Transform,false);

            _firstPersonController.Move(new Vector2(transform.forward.x+Mathf.Sin(Mathf.PI/Random.Range(1,4)), 
                transform.forward.z + Mathf.Cos(Mathf.PI / Random.Range(1, 4)))); 
        }

        public void GetDamage(float damage)
        {
            if (Health <= 0)
                return;

            Health = Mathf.Clamp(Health - damage, 0, _maxHealth);
        }

        public void SetCursorLock(bool flag)
        {
            _firstPersonController.SetCursorLock(flag);
        }
    }
}
