using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using TutorialFPS.Services;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace TutorialFPS.Models
{
    public class PlayerModel : BaseGameObject, IDamagable
    {
        [SerializeField]
        private float _maxHealth = 100f;

        private float _health;

        public float Health
        {
            get { return _health; }
            private set { _health = value; }
        }

        private void Start()
        {
            Health = _maxHealth;
            Main.Instance.Notify(Notification.UpdateHealth, this);
        }

        public void Death()
        {
            RigidbodyFirstPersonController firstPersonController= GetComponent<RigidbodyFirstPersonController>();
            firstPersonController.enabled = false;
            Main.Instance.InputController.enabled = false;
            Rigidbody.constraints = RigidbodyConstraints.None;

            firstPersonController.Move(new Vector2(transform.forward.x+Mathf.Sin(Mathf.PI/Random.Range(1,4)), 
                transform.forward.z + Mathf.Cos(Mathf.PI / Random.Range(1, 4)))); 
        }

        public void GetDamage(float damage)
        {
            if (Health <= 0)
                return;

            Health = Mathf.Clamp(Health - damage, 0, _maxHealth);

            Main.Instance.Notify(Notification.UpdateHealth, this);
        }
    }
}
