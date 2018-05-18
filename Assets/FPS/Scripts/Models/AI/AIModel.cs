using System;
using System.Collections;
using System.Collections.Generic;
using TutorialFPS;
using TutorialFPS.Interfaces;
using TutorialFPS.Services.Data;
using UnityEngine;
using UnityEngine.AI;

namespace TutorialFPS.Models.AI
{
    public class AIModel : BaseGameObject, IDamagable, ISavable
    {
        public Transform eyes;
        public float maxHideDistance = 10f;
        public int maxHideRays = 20;
        public float attackTime = 1f;
        public float timeBetweenAttack = 4f;
        public float walkTurnSpeed = 40f;
        public float attackTurnSpeed = 70f;
        public float fightingOutOfVisibleTime = 10f;
        public float patrolSpeed = 2f;
        public float fightSpeed = 4f;

        [HideInInspector] public int currentAiBehaviour;
        [HideInInspector] public int remainInBehaviour;
        [HideInInspector] public CapsuleCollider enemy;
        [HideInInspector] public Animator Animator;
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
        private Data _data;
        private AudioSource _audioSource;

        public float Health { get; private set; }

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
                    WeaponsMagazine = new []{weapon.Magazine},
                    IsVisible = IsVisible,
                    NextWayPoint=nextWayPoint,
                    CurrentAiBehaviour=currentAiBehaviour
                };
            }
            set
            {
                Position = value.Position;
                Rotation = value.Rotation;
                Scale = value.Scale;
                Health = value.HitPoints;
                weapon.Magazine = value.WeaponsMagazine[0];
                IsVisible = value.IsVisible;
                nextWayPoint = value.NextWayPoint;
                currentAiBehaviour = value.CurrentAiBehaviour;
            }
        }

        protected override void SetVisibility(Transform objTransform, bool visible)
        {
            base.SetVisibility(objTransform, visible);

            isDead = !visible;
            if (!navMeshAgent.enabled && visible)
            {
                navMeshAgent.enabled = true;
                ResetCountDown();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            navMeshAgent = GetComponent<NavMeshAgent>();
            weapon = GetComponentInChildren<WeaponModel>();
            Animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            Health = _maxHealth;
        }

        private void Update()
        {
            if (isDead)
            {
                return;
            }

            if (navMeshAgent.isOnOffMeshLink && navMeshAgent.currentOffMeshLinkData.offMeshLink.activated)
            {
                Animator.SetTrigger("Jump");
                navMeshAgent.currentOffMeshLinkData.offMeshLink.activated = false;
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

            Animator.SetTrigger("Hurt");
            _audioSource.Stop();
            _audioSource.Play();
            Health = Mathf.Clamp(Health - damage, 0, _maxHealth);
        }

        public void SetAgentActive(bool isActive)
        {
            navMeshAgent.isStopped = !isActive;
            Animator.SetBool("IsStopped", !isActive);
        }

        public void Death()
        {
            StopAllCoroutines();
            isDead = true;
            navMeshAgent.enabled = false;
            Animator.SetTrigger("Death");
            Invoke("OnDeath", 4f);
        }

        private void OnDeath()
        {
            SetVisibility(Transform, false);
        }
    }
}

