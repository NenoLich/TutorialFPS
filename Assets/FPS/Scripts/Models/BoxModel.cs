using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using TutorialFPS.Services.Data;
using UnityEngine;

namespace TutorialFPS.Models
{
    public class BoxModel : PickableModel, IDamagable, ISavable
    {
        [SerializeField]
        private float _maxHealth = 100f;

        private float _health;
        private Color _defaultColor;
        private bool isDead = false;
        private AudioSource _audioSource;

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
                    HitPoints = _health,
                    IsVisible = IsVisible
                };
            }
            set
            {
                Position = value.Position;
                Rotation = value.Rotation;
                Scale = value.Scale;
                _health = value.HitPoints;
                IsVisible = value.IsVisible;
            }
        }

        protected override void SetVisibility(Transform objTransform, bool visible)
        {
            base.SetVisibility(objTransform, visible);

            isDead = !visible;
            if (visible)
            {
                Rigidbody.velocity = Vector3.zero;
                Color = _defaultColor;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _health = _maxHealth;
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!isDead && _health == 0)
            {
                Death();
            }
        }

        private void Death()
        {
            isDead = true;
            _defaultColor = Color;
            Color = Color.red;
            GetComponent<Collider>().enabled = false;
            Invoke("OnDeath", 2f);
        }

        private void OnDeath()
        {
            SetVisibility(Transform, false);
        }

        public void GetDamage(float damage)
        {
            if (_health <= 0)
                return;

            _audioSource.Stop();
            _audioSource.Play();
            _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
        }
    }
}