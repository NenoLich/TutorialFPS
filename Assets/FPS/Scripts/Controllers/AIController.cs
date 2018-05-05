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
        public Transform[] waypoints;
        public CapsuleCollider EnemyCollider;

        private Transform _agentTransform;
        private Animator _animator;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            foreach (AIModel model in aiModels)
            {
                model.waypoints = waypoints;
                model.enemy = EnemyCollider;
            }
        }
    }
}
