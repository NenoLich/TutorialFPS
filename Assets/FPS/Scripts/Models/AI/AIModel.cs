using System;
using System.Collections;
using System.Collections.Generic;
using TutorialFPS;
using TutorialFPS.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace TutorialFPS.Models.AI
{
    public class AIModel : BaseGameObject,IDamagable
    {
        public AIBehaviour currentBehaviour;
        public Transform eyes;
        public AIBehaviour remainInBehaviour;
        public float maxHideDistance = 10f;
        public int maxHideRays = 20;
        public float attackTime = 1f;
        public float timeBetweenAttack = 4f;
        public float attackTurnSpeed = 70f;
        public float fightingOutOfVisibleTime = 10f;

        [HideInInspector] public CapsuleCollider enemy;
        [HideInInspector] public Vector3 target;
        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public Transform[] waypoints;
        [HideInInspector] public int nextWayPoint;
        [HideInInspector] public WeaponModel weapon;
        [HideInInspector] public float lastTimeTargetUpdated;
        [HideInInspector] public bool isDead;

        [SerializeField]
        private float _maxHealth = 100f;
        private float timeElapsed;
        private float lastTimeElapsed;

        public float Health { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            navMeshAgent = GetComponent<NavMeshAgent>();
            weapon = GetComponentInChildren<WeaponModel>();
            Health = _maxHealth;
        }

        private void Update()
        {
            currentBehaviour.UpdateBehaviour(this);
        }

        public void TransitionToBehaviour(AIBehaviour nextBehaviour)
        {
            if (nextBehaviour != remainInBehaviour)
            {
                currentBehaviour = nextBehaviour;
                ResetCountDown();
            }
        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            if (lastTimeElapsed != 0)
            {
                timeElapsed += Time.time - lastTimeElapsed;
            }
            lastTimeElapsed = Time.time;
            return (timeElapsed >= duration);
        }

        public void ResetCountDown()
        {
            timeElapsed = 0;
            lastTimeElapsed = 0;
            lastTimeTargetUpdated = Time.time;
        }

        public void Coroutine(Func<IEnumerator> routine)
        {
            StartCoroutine(routine());
        }

        public void UpdateTarget(Vector3 newTarget)
        {
            target = newTarget;
            lastTimeTargetUpdated = Time.time;
        }

        public void GetDamage(float damage)
        {
            if (Health <= 0)
                return;

            Health = Mathf.Clamp(Health - damage, 0, _maxHealth);
        }
    }
}

