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
        public float attackTurnSpeed = 5f;
        public float fightingOutOfVisibleTime = 10f;
        public float patrolSpeed = 2f;
        public float fightSpeed = 4f;

        [HideInInspector] public int currentAiBehaviour;
        [HideInInspector] public int remainInBehaviour;
        [HideInInspector] public CapsuleCollider enemy;
        [HideInInspector] public Animator Animator;
        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public Transform[] waypoints;
        [HideInInspector] public int nextWayPoint;
        [HideInInspector] public WeaponModel weapon;
        [HideInInspector] public float lastTimeTargetUpdated;
        [HideInInspector] public bool isDead;
        [HideInInspector] public bool isDamaged;

        [SerializeField]
        private float _maxHealth = 100f;
        [SerializeField]
        private ParticleSystem _blood;

        private Vector3 _target;
        private float timeElapsed;
        private float lastTimeElapsed;
        private Data _data;
        private AudioSource _audioSource;

        public float Health { get; private set; }

        public Vector3 Target
        {
            get { return _target; }
        }

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

        private void LateUpdate()
        {
            isDamaged = false;
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
            _target = newTarget;
            lastTimeTargetUpdated = Time.time;
        }

        public void GetDamage(float damage,Vector3 source)
        {
            if (Health <= 0)
                return;

            _blood.Stop();
            _blood.transform.position = source;
            _blood.transform.rotation =
                Quaternion.LookRotation(new Vector3((source - Position).x, source.y, (source - Position).z));
            _blood.Play();

            _audioSource.Stop();
            _audioSource.Play();
            Health = Mathf.Clamp(Health - damage, 0, _maxHealth);
            Animator.SetTrigger("Hurt");

            isDamaged = true;
            UpdateTarget(Camera.main.transform.position);
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
            SetAgentActive(false);
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

