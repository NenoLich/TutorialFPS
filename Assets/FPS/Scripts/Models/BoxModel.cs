using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS.Models
{
    public class BoxModel : PickableModel, IDamagable
    {
        [SerializeField]
        private float _maxHealth = 100f;

        private float _health;

        protected override void Awake()
        {
            base.Awake();
            _health = _maxHealth;
        }

        private void Death()
        {
            Color = Color.red;
            GetComponent<Collider>().enabled = false;
            Destroy(InstanceObject, 3f);
        }

        public void GetDamage(float damage)
        {
            if (_health <= 0)
                return;
            
            _health = Mathf.Clamp(_health - damage, 0, _maxHealth);

            if (_health == 0)
                Death();
        }
    }
}