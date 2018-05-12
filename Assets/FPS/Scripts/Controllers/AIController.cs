using System;
using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Models.AI;
using UnityEngine;
using UnityEngine.AI;

namespace TutorialFPS.Controllers
{
    public class AIController : BaseController
    {
        public AIModel[] aiModels;
        public AIBehaviour[] aiBehaviours;
        public Transform[] waypoints;
        public CapsuleCollider EnemyCollider;

        [SerializeField] private int remainInBehaviourIndex = 0;
        [SerializeField] private int startAiBehaviourIndex = 0;
        private Transform _agentTransform;
        private Animator _animator;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            foreach (AIModel model in aiModels)
            {
                model.remainInBehaviour = remainInBehaviourIndex;
                model.currentAiBehaviour = startAiBehaviourIndex;
                model.waypoints = waypoints;
                model.enemy = EnemyCollider;
            }
        }

        private void Update()
        {
            foreach (AIModel aiModel in aiModels)
            {
                aiBehaviours[aiModel.currentAiBehaviour].UpdateBehaviour(aiModel, this);
            }
        }

        public void TransitionToBehaviour(AIBehaviour nextBehaviour,AIModel aiModel)
        {
            if (nextBehaviour != aiBehaviours[aiModel.remainInBehaviour])
            {
                aiModel.currentAiBehaviour = Array.IndexOf(aiBehaviours, nextBehaviour);
                aiModel.ResetCountDown();
            }
        }
    }
}
